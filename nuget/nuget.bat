nuget pack Transformalize.Provider.Json.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Provider.Json.Autofac.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Provider.Json.Autofac.v3.nuspec -OutputDirectory "c:\temp\modules"

REM nuget push "c:\temp\modules\Transformalize.Provider.Json.0.6.11-beta.nupkg" -source https://api.nuget.org/v3/index.json
REM nuget push "c:\temp\modules\Transformalize.Provider.Json.Autofac.0.6.11-beta.nupkg" -source https://api.nuget.org/v3/index.json
REM nuget push "c:\temp\modules\Transformalize.Provider.Json.Autofac.v3.0.6.11-beta.nupkg" -source https://api.nuget.org/v3/index.json
