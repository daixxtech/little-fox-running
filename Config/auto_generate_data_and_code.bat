del /s /q ..\Assets\Config\*.json
del /s /q ..\Assets\Scripts\Config\*.cs

..\Tools\excel-translator\ExcelTranslator.exe -e ../Config -j ../Assets/Config -c ../Assets/Scripts/Config -p Conf

pause