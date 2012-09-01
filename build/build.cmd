del build.log
mkdir ..\out

devenv.exe ..\src\XamlIntegRT.sln /build Release /out build.log
..\tools\nuget\NuGet.exe pack XamlIntegRT.nuspec -o ..\out