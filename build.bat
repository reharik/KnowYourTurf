@ECHO OFF

REM By default build file will run BUILD, but the given argument will be used as the TARGET
REM for example:  build.bat stage …will run the STAGE target build

SET COMMAND_PATH=%SystemRoot%\Microsoft.NET\Framework64\v4.0.30319\MSBuild
SET PROJECT="build.proj "
SET BUILDARGS=

IF  NOT "%1"  == ""          SET BUILDARGS= /target:%1
ECHO Running:  %COMMAND_PATH% build.proj  %BUILDARGS%
%COMMAND_PATH% build.proj  %BUILDARGS%

