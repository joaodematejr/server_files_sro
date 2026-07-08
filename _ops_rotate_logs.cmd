@echo off
cd /d %~dp0
powershell.exe -NoProfile -ExecutionPolicy Bypass -File ".\ops\rotate_logs.ps1"
