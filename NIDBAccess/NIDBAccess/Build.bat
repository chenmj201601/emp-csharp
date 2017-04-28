Set BuildProject=NIDBAccess.csproj
@echo Build %BuildProject%
@date /t
@time /t

devenv NIDBAccess.csproj /rebuild "Release|AnyCPU" /project NIDBAccess /out "BuildInfo.txt"
@if errorlevel 1 goto error
@Set BuildSuc=1
@goto end

:error
@Set BuildSuc=0

:end
@echo Build End