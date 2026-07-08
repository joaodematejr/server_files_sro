param(
    [int]$RecentMinutes = 30
)

$ErrorActionPreference = "Stop"

$processes = @(
    "CustomCertificationServer",
    "GlobalManager",
    "MachineManager",
    "DownloadServer",
    "GatewayServer",
    "FarmManager",
    "AgentServer",
    "SR_ShardManager",
    "SR_GameServer"
)

$ports = @(32000, 15880, 15882, 15883, 8090)
$fatalKeywords = @("OnExit() Called", "TopLevelFilter", "cannot establish keep alive session", "Failed to initialize billing server")

$base = Split-Path -Parent $PSScriptRoot
$fatalLog = Join-Path $base "2026-07-08_FatalLog.txt"
$exceptionLog = Join-Path $base "ExceptionHandlerInfo.log"

$failed = $false
Write-Host "=== HEALTH CHECK ==="
Write-Host "Time: $(Get-Date -Format 'yyyy-MM-dd HH:mm:ss')"
Write-Host ""

Write-Host "[1] Processos"
foreach ($p in $processes) {
    $proc = Get-Process -Name $p -ErrorAction SilentlyContinue
    if ($null -eq $proc) {
        Write-Host "[FAIL] Processo ausente: $p"
        $failed = $true
    }
    else {
        Write-Host "[ OK ] $p PID=$($proc.Id -join ',')"
    }
}
Write-Host ""

Write-Host "[2] Portas"
$tcp = Get-NetTCPConnection -State Listen -ErrorAction SilentlyContinue
foreach ($port in $ports) {
    $hit = $tcp | Where-Object { $_.LocalPort -eq $port }
    if ($null -eq $hit) {
        Write-Host "[FAIL] Porta sem listener: $port"
        $failed = $true
    }
    else {
        $ips = ($hit | Select-Object -ExpandProperty LocalAddress -Unique) -join ","
        Write-Host "[ OK ] Porta $port em $ips"
    }
}
Write-Host ""

Write-Host "[3] Eventos criticos recentes"
$since = (Get-Date).AddMinutes(-1 * $RecentMinutes)
$criticalCount = 0

if (Test-Path $fatalLog) {
    $lines = Get-Content $fatalLog -Tail 1200
    $recentLines = @()
    foreach ($ln in $lines) {
        if ($ln -match '^(?<d>\d{4}-\d{2}-\d{2})\s+(?<t>\d{2}:\d{2}:\d{2})') {
            $stamp = [datetime]::ParseExact("$($matches.d) $($matches.t)", "yyyy-MM-dd HH:mm:ss", $null)
            if ($stamp -ge $since) {
                $recentLines += $ln
            }
        }
    }

    foreach ($k in $fatalKeywords) {
        $hits = $recentLines | Select-String -SimpleMatch $k
        if ($hits) {
            $criticalCount += $hits.Count
            Write-Host "[WARN] FatalLog recente contem '$k' ($($hits.Count)x)"
        }
    }
}

if (Test-Path $exceptionLog) {
    $recentEx = Get-Content $exceptionLog -Tail 300 | Where-Object {
        if ($_ -match '^(?<d>\d{4}-\d{2}-\d{2})\s+(?<t>\d{2}:\d{2}:\d{2})') {
            $stamp = [datetime]::ParseExact("$($matches.d) $($matches.t)", "yyyy-MM-dd HH:mm:ss", $null)
            return $stamp -ge $since
        }
        return $false
    }
    if ($recentEx.Count -gt 0) {
        Write-Host "[WARN] ExceptionHandlerInfo possui registros na janela recente"
    }
}

if ($criticalCount -eq 0) {
    Write-Host "[ OK ] Sem palavras-chave criticas no recorte analisado"
}
Write-Host ""

if ($failed) {
    Write-Host "RESULTADO: FAIL" -ForegroundColor Red
    exit 1
}

Write-Host "RESULTADO: OK" -ForegroundColor Green
exit 0
