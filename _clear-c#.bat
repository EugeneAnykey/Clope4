@echo off

echo deleting 'bin' folders...
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S bin') DO RMDIR /S /Q "%%G"
@echo    done.

echo deleting 'obj' folders...
FOR /F "tokens=*" %%G IN ('DIR /B /AD /S obj') DO RMDIR /S /Q "%%G"
@echo    done.

pause
