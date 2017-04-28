echo off
@echo ...
@echo Start Build
@date /t
@time /t


@echo Module Common Start Build


@echo ====================
@echo EMPReport
cd EMPReport\EMPReport
@call Build.bat
cd ..\..
if %BuildSuc% == 0 goto error
@echo %BuildProject% BUILD END

@echo ====================
@echo EMPReportControl
cd EMPReport\EMPReportControl
@call Build.bat
cd ..\..
if %BuildSuc% == 0 goto error
@echo %BuildProject% BUILD END


@echo CoModule Common Build End

@goto end



:error
@Set BuildSuc=0

:end
@echo Build End