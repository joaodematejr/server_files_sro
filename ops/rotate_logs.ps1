param(
    [int]$RetentionDays = 14
)

$ErrorActionPreference = "Stop"

$base = Split-Path -Parent $PSScriptRoot
$now = Get-Date
$cutoff = $now.AddDays(-1 * $RetentionDays)

$dumpDir = Join-Path $base "Log\Dump"
$fatalDir = Join-Path $base "Log\FatalLog"
$reportDir = Join-Path $base "Log\ReportLog"
$archiveDir = Join-Path $base "Log\Archive"
$archiveStamp = Get-Date -Format "yyyyMMdd_HHmmss"
$archiveRunDir = Join-Path $archiveDir $archiveStamp

$null = New-Item -ItemType Directory -Force -Path $dumpDir, $fatalDir, $reportDir, $archiveRunDir

Write-Host "=== LOG ROTATION ==="
Write-Host "Base: $base"
Write-Host "RetentionDays: $RetentionDays"
Write-Host ""

# 1) Move dump files from root to Log/Dump
$rootDumps = Get-ChildItem -Path $base -File -Filter "*.dmp" -ErrorAction SilentlyContinue
foreach ($f in $rootDumps) {
    $target = Join-Path $dumpDir $f.Name
    Move-Item -Force -Path $f.FullName -Destination $target
    Write-Host "[MOVE] Dump -> Log/Dump: $($f.Name)"
}

# 2) Move old txt logs from root to Log/FatalLog
$rootFatal = Get-ChildItem -Path $base -File -Filter "*_FatalLog.txt" -ErrorAction SilentlyContinue
foreach ($f in $rootFatal) {
    if ($f.LastWriteTime -lt $cutoff) {
        $target = Join-Path $fatalDir $f.Name
        Move-Item -Force -Path $f.FullName -Destination $target
        Write-Host "[MOVE] Fatal antigo -> Log/FatalLog: $($f.Name)"
    }
}

# 3) Archive old files from log folders
$logFolders = @($dumpDir, $fatalDir, $reportDir)
foreach ($folder in $logFolders) {
    $oldFiles = Get-ChildItem -Path $folder -File -ErrorAction SilentlyContinue | Where-Object { $_.LastWriteTime -lt $cutoff }
    foreach ($f in $oldFiles) {
        $target = Join-Path $archiveRunDir $f.Name
        Move-Item -Force -Path $f.FullName -Destination $target
        Write-Host "[ARCHIVE] $($f.FullName) -> $target"
    }
}

# 4) Keep at most last 20 archive folders
$archives = Get-ChildItem -Path $archiveDir -Directory -ErrorAction SilentlyContinue | Sort-Object LastWriteTime -Descending
if ($archives.Count -gt 20) {
    $toDelete = $archives | Select-Object -Skip 20
    foreach ($d in $toDelete) {
        Remove-Item -Recurse -Force -Path $d.FullName
        Write-Host "[CLEAN] Removed old archive folder: $($d.Name)"
    }
}

Write-Host ""
Write-Host "Rotation done."
