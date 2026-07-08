@echo off

:: Billing mock server (local fallback)
start "BillingMock" powershell.exe -NoProfile -ExecutionPolicy Bypass -File ".\billing_mock.ps1"
timeout /nobreak /t 1

:: CertificationServer
start .\Cert\CustomCertificationServer.exe .\Cert\packt.dat
timeout /nobreak /t 2

start GlobalManager.exe
timeout /nobreak /t 2
start MachineManager.exe
timeout /nobreak /t 2
start DownloadServer.exe
timeout /nobreak /t 2
start GatewayServer.exe
timeout /nobreak /t 2
start FarmManager.exe
timeout /nobreak /t 2
start AgentServer.exe
timeout /nobreak /t 2
start SR_ShardManager.exe
timeout /nobreak /t 5
start SR_GameServer.exe
timeout /nobreak /t 10

:: SMC
cd .\SMC
start SMC.exe
