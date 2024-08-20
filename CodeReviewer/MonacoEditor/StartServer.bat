@echo off
REM Check if Python is installed
python --version >nul 2>&1
IF ERRORLEVEL 1 (
    echo Python is not installed. Please install Python to use this script.
    exit /b
)

REM Change to the directory where the script is located
cd /d "%~dp0"

REM Start the web server
python webserver.py

echo Server Shutdown
