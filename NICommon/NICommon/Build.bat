Set BuildProject=NICommon.csproj
@echo Build %BuildProject%
@date /t
@time /t

devenv NICommon.csproj /rebuild "Release|AnyCPU" /project NICommon /out "BuildInfo.txt"
@if errorlevel 1 goto error
@Set BuildSuc=1
@goto end

:error
@Set BuildSuc=0

:end
@echo Build End