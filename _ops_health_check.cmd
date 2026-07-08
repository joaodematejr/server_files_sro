@echo off
cd /d %~dp0
powershell.exe -NoProfile -ExecutionPolicy Bypass -File ".\ops\health_check.ps1"
