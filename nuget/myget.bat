nuget pack Transformalize.Provider.Json.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Provider.Json.Autofac.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Transform.Json.nuspec -OutputDirectory "c:\temp\modules"
nuget pack Transformalize.Transform.Json.Autofac.nuspec -OutputDirectory "c:\temp\modules"

nuget push "c:\temp\modules\Transformalize.Provider.Json.0.10.1-beta.nupkg" -source https://www.myget.org/F/transformalize/api/v3/index.json
nuget push "c:\temp\modules\Transformalize.Provider.Json.Autofac.0.10.1-beta.nupkg" -source https://www.myget.org/F/transformalize/api/v3/index.json
nuget push "c:\temp\modules\Transformalize.Transform.Json.0.10.1-beta.nupkg" -source https://www.myget.org/F/transformalize/api/v3/index.json
nuget push "c:\temp\modules\Transformalize.Transform.Json.Autofac.0.10.1-beta.nupkg" -source https://www.myget.org/F/transformalize/api/v3/index.json
