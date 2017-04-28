echo off
@echo ...
@echo Start Build
@date /t
@time /t


@echo Common Library Start Build


@echo ====================
@echo NICommon
cd NICommon\NICommon
@call Build.bat
cd ..\..
if %BuildSuc% == 0 goto error
@echo %BuildProject% BUILD END

@echo ====================
@echo NIRibbon
cd NIRibbon\NIRibbon
@call Build.bat
cd ..\..
if %BuildSuc% == 0 goto error
@echo %BuildProject% BUILD END

@echo ====================
@echo NIWindowsShells
cd NIAvalonDock\NIWindowsShells
@call Build.bat
cd ..\..
if %BuildSuc% == 0 goto error
@echo %BuildProject% BUILD END

@echo ====================
@echo NIAvalonDock
cd NIAvalonDock\NIAvalonDock
@call Build.bat
cd ..\..
if %BuildSuc% == 0 goto error
@echo %BuildProject% BUILD END

@echo ====================
@echo NICustomControls
cd NICustomControls\NICustomControls
@call Build.bat
cd ..\..
if %BuildSuc% == 0 goto error
@echo %BuildProject% BUILD END

@echo ====================
@echo NIDBAccess
cd NIDBAccess\NIDBAccess
@call Build.bat
cd ..\..
if %BuildSuc% == 0 goto error
@echo %BuildProject% BUILD END


@echo Common Library Build End

@goto end



:error
@Set BuildSuc=0

:end
@echo Build End