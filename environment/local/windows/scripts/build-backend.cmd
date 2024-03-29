@echo off
REM This script is intended for development purposes only.
REM It is not intended for production use.


REM -- Variables --

set RPDEV_prefix=[96m[RPDEV][0m
set RPDEV_backend_path=%~dp0..\..\..\..\rpdev-backend\
set RPDEV_devenv_path=%~dp0..\devenv\


REM -- Build --

echo %RPDEV_prefix% [91mBuilding backend...[0m
cd %RPDEV_backend_path%
dotnet restore
dotnet publish rpdev-backend.csproj -c Release -o %RPDEV_devenv_path% --self-contained true -p:PublishSingleFile=true -p:AssemblyName=rpdev-backend
echo %RPDEV_prefix% [92mBuilding backend... done[0m

cd %~dp0