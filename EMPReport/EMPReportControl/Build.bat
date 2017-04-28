Set BuildProject=EMPReportControl.csproj
@echo Build %BuildProject%
@date /t
@time /t

devenv EMPReportControl.csproj /rebuild "Release|AnyCPU" /project EMPReportControl /out "BuildInfo.txt"
@if errorlevel 1 goto error
@Set BuildSuc=1
@goto end

:error
@Set BuildSuc=0

:end
@echo Build End