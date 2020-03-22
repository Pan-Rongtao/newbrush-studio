@echo off
setlocal enabledelayedexpansion

set TOOLDIR=..\..\tools
set PROTOC_EXE=%TOOLDIR%\protoc.exe
set GRPC_PLUGIN=%TOOLDIR%\grpc_csharp_plugin.exe

for /f %%i in ('dir /b /a-d *.proto ^| find "." /c') do set "num=%%i"
echo ���账��[%num%]��proto�ļ�

echo ��ʼ����API...
echo.

set /a v=0
for /r "." %%i in (*.proto) do (
echo ���ڴ���[!v!/%num%] %%i
::��ȡ�ļ���
for %%a in ("%%i") do set "file=%%~na.proto"
%PROTOC_EXE% -I=. --grpc_out=. --plugin=protoc-gen-grpc=%GRPC_PLUGIN% !file!
set /a v+=1
)

echo.
echo ��ɣ�

@pause