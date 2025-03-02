@echo off
echo 署名を実行しています...
echo 対象ファイル: %1

:: SignToolのパスを見つける
for /f "delims=" %%i in ('where /r "C:\Program Files (x86)\Windows Kits\10" signtool.exe') do (
    set SIGNTOOL=%%i
    goto :found
)

:found
if "%SIGNTOOL%"=="" (
    echo SignToolが見つかりませんでした。
    exit /b 1
)

:: アプリケーションに署名
"%SIGNTOOL%" sign /f "%CERTIFICATE_PATH%" /p %CERTIFICATE_PASSWORD% /fd sha256 /tr http://timestamp.digicert.com /td sha256 %1

if %ERRORLEVEL% neq 0 (
    echo 署名プロセスに失敗しました。エラーコード: %ERRORLEVEL%
    exit /b %ERRORLEVEL%
)

echo 署名が完了しました。
exit /b 0