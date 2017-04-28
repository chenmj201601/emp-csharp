echo off
@echo ...
@echo Start Build
@date /t
@time /t

@echo Common Library Start Build
@call BuildCommonLib.bat
if %BuildSuc% == 0 goto error



@echo Module Common Build Start
@call BuildModuleCommon.bat
if %BuildSuc% == 0 goto error


@echo App Build Start
@call BuildApp.bat
if %BuildSuc% == 0 goto error



@goto Finish

:error
@echo FAILED TO %BuildProject%
goto End

:Finish
@echo ALL BUILD END
@date /t
@time /t
:End