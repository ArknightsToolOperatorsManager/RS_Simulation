@echo off
echo ���������s���Ă��܂�...
echo �Ώۃt�@�C��: %1

:: SignTool�̃p�X��������
for /f "delims=" %%i in ('where /r "C:\Program Files (x86)\Windows Kits\10" signtool.exe') do (
    set SIGNTOOL=%%i
    goto :found
)

:found
if "%SIGNTOOL%"=="" (
    echo SignTool��������܂���ł����B
    exit /b 1
)

:: �A�v���P�[�V�����ɏ���
"%SIGNTOOL%" sign /f "%CERTIFICATE_PATH%" /p %CERTIFICATE_PASSWORD% /fd sha256 /tr http://timestamp.digicert.com /td sha256 %1

if %ERRORLEVEL% neq 0 (
    echo �����v���Z�X�Ɏ��s���܂����B�G���[�R�[�h: %ERRORLEVEL%
    exit /b %ERRORLEVEL%
)

echo �������������܂����B
exit /b 0