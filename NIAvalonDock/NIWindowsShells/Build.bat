Set BuildProject=NIWindowsShells.csproj
@echo Build %BuildProject%
@date /t
@time /t

devenv NIWindowsShells.csproj /rebuild "Release|AnyCPU" /project NIWindowsShells /out "BuildInfo.txt"
@if errorlevel 1 goto error
@Set BuildSuc=1
@goto end

:error
@Set BuildSuc=0

:end
@echo Build End