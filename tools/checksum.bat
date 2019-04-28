rem Usage: checksum.bat ..\Focusu.GUI\bin\Release\Focusu-0.1.0.zip
echo off
set target_file=%1
FOR /f "tokens=* skip=1" %%i in ('CertUtil -hashfile .\%target_file% SHA1') DO ( echo %%i > %1.SHA1.txt && exit)
