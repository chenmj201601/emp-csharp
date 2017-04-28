echo off
@echo ...
@echo Start Build
@date /t
@time /t


@echo App Start Build


@echo ====================
@echo ReportDesigner
cd EMPReport\ReportDesigner
@call Build.bat
cd ..\..
if %BuildSuc% == 0 goto error
@echo %BuildProject% BUILD END


@echo App  Build End

@goto end



:error
@Set BuildSuc=0

:end
@echo Build End