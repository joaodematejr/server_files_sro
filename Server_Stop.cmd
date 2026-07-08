@echo off

taskkill /f /fi "WINDOWTITLE eq BillingMock*"
timeout /nobreak /t 1
taskkill /f /im SMC.exe
timeout /nobreak /t 1
taskkill /f /im SR_GameServer.exe
timeout /nobreak /t 1
taskkill /f /im SR_ShardManager.exe
timeout /nobreak /t 1
taskkill /f /im AgentServer.exe
timeout /nobreak /t 1
taskkill /f /im FarmManager.exe
timeout /nobreak /t 1
taskkill /f /im GatewayServer.exe
timeout /nobreak /t 1
taskkill /f /im DownloadServer.exe
timeout /nobreak /t 1
taskkill /f /im MachineManager.exe
timeout /nobreak /t 1
taskkill /f /im GlobalManager.exe
timeout /nobreak /t 1
taskkill /f /im CustomCertificationServer.exe
