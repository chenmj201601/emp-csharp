Set BuildProject=ReportDesigner.csproj
@echo Build %BuildProject%
@date /t
@time /t

devenv ReportDesigner.csproj /rebuild "Release|AnyCPU" /project ReportDesigner /out "BuildInfo.txt"
@if errorlevel 1 goto error
@Set BuildSuc=1
@goto end

:error
@Set BuildSuc=0

:end
@echo Build End