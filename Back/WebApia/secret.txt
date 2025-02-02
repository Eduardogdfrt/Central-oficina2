@@ -1,23 +0,0 @@
@echo off
SETLOCAL ENABLEDELAYEDEXPANSION

:: Caminho do arquivo appsettings.json
SET APPSETTINGS_FILE=appsettings.json

:: Dividir a ClientSecret em duas partes
SET PART1=TxA8Q~7tgHIsiQeuT
SET PART2=BiGjqLhp5oxbRCgqAfOHbOe

:: Juntar as duas partes da ClientSecret
SET CLIENTSECRET=%PART1%%PART2%

:: Exibir valor da ClientSecret para debug
echo ClientSecret a ser colocado no appsettings.json: !CLIENTSECRET!

:: Substituir a ClientSecret no appsettings.json
powershell -Command "(Get-Content %APPSETTINGS_FILE%) -replace '\"ClientSecret\": \"\"', '\"ClientSecret\": \"!CLIENTSECRET!\"' | Set-Content %APPSETTINGS_FILE%"

powershell -Command "(Get-Content %APPSETTINGS_FILE%) -replace '\"ClientSecret\": \"\"', '\"ClientSecret\": \"!CLIENTSECRET!\"' | Set-Content %APPSETTINGS_FILE%"

echo ClientSecret atualizado no appsettings.json com sucesso!
ENDLOCAL