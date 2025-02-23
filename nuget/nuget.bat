REM nuget pack Transformalize.Provider.Json.nuspec -OutputDirectory "c:\temp\modules"
REM nuget pack Transformalize.Provider.Json.Autofac.nuspec -OutputDirectory "c:\temp\modules"
REM nuget pack Transformalize.Transform.Json.nuspec -OutputDirectory "c:\temp\modules"
REM nuget pack Transformalize.Transform.Json.Autofac.nuspec -OutputDirectory "c:\temp\modules"

REM nuget push "c:\temp\modules\Transformalize.Provider.Json.0.11.1-beta.nupkg" -source https://api.nuget.org/v3/index.json
REM nuget push "c:\temp\modules\Transformalize.Provider.Json.Autofac.0.11.1-beta.nupkg" -source https://api.nuget.org/v3/index.json
REM nuget push "c:\temp\modules\Transformalize.Transform.Json.0.11.1-beta.nupkg" -source https://api.nuget.org/v3/index.json
REM nuget push "c:\temp\modules\Transformalize.Transform.Json.Autofac.0.11.1-beta.nupkg" -source https://api.nuget.org/v3/index.json
