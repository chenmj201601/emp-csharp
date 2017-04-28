Set BuildProject=NICustomControls.csproj
@echo Build %BuildProject%
@date /t
@time /t

devenv NICustomControls.csproj /rebuild "Release|AnyCPU" /project NICustomControls /out "BuildInfo.txt"
@if errorlevel 1 goto error
@Set BuildSuc=1
@goto end

:error
@Set BuildSuc=0

:end
@echo Build End