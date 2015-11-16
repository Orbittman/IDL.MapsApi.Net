@echo Off
set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)
REM Package restore
call %NuGet% restore IDL.MapsApi.Net\IDL.MapsApi.Net.Tests\packages.config -OutputDirectory %cd%\packages -NonInteractive

REM Build
%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild IDL.MapsApi.Net.sln /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Package
mkdir Build
call %nuget% pack "IDL.MapsApi.Net\IDL.MapsApi.Net\IDL.MapsApi.Net.csproj" -symbols -o Build -p Configuration=%config% %version%
