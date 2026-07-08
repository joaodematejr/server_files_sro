@echo off

set DMP_DIR=Dump
set FATAL_DIR=FatalLog
set REPORT_DIR=ReportLog
Set CCU_DIR=ConcurrentUser

if not exist Log		mkdir Log

if not exist Log\%DMP_DIR%	mkdir Log\%DMP_DIR%
if not exist Log\%FATAL_DIR%	mkdir Log\%FATAL_DIR%
if not exist Log\%REPORT_DIR%	mkdir Log\%REPORT_DIR%
if not exist Log\%CCU_DIR%		mkdir Log\%CCU_DIR%

if exist *.dmp 			move /Y *.dmp 			Log\%DMP_DIR% >NUL
if exist *_FatalLog.txt 	move /Y *_FatalLog.txt 		Log\%FATAL_DIR% >NUL
if exist ReportLog_*.txt 	move /Y ReportLog_*.txt 	Log\%REPORT_DIR% >NUL
if exist ConcurrentUser_*.txt	move /Y ConcurrentUser_*.txt	Log\%CCU_DIR% >NUL
