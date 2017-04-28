Set BuildProject=NIRibbon.csproj
@echo Build %BuildProject%
@date /t
@time /t

devenv NIRibbon.csproj /rebuild "Release|AnyCPU" /project NIRibbon /out "BuildInfo.txt"
@if errorlevel 1 goto error
@Set BuildSuc=1
@goto end

:error
@Set BuildSuc=0

:end
@echo Build End