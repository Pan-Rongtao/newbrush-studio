@echo off
setlocal enabledelayedexpansion

set TOOLDIR=..\..\tools
set PROTOC_EXE=%TOOLDIR%\protoc.exe
set GRPC_PLUGIN=%TOOLDIR%\grpc_csharp_plugin.exe

for /f %%i in ('dir /b /a-d *.proto ^| find "." /c') do set "num=%%i"
echo 共需处理[%num%]个proto文件

echo 开始生成API...
echo.

set /a v=0
for /r "." %%i in (*.proto) do (
echo 正在处理[!v!/%num%] %%i
::提取文件名
for %%a in ("%%i") do set "file=%%~na.proto"
%PROTOC_EXE% -I=. --grpc_out=. --plugin=protoc-gen-grpc=%GRPC_PLUGIN% !file!
set /a v+=1
)

echo.
echo 完成！

@pause