#load "tools/includes.fsx"
open IntelliFactory.Build

let bt = BuildTool().PackageId("WebSharper.WebApi", "2.5-alpha")

let main =
    bt.FSharp.Library("IntelliFactory.WebSharper.WebApi")
        .SourcesFromProject()
        .References(fun r ->
            let wsPaths =
                [
                    "tools/net40/IntelliFactory.Html.dll"
                    "tools/net40/IntelliFactory.JavaScript.dll"
                    "tools/net40/IntelliFactory.WebSharper.dll"
                    "tools/net40/IntelliFactory.WebSharper.Core.dll"
                    "tools/net40/IntelliFactory.WebSharper.Sitelets.dll"
                    "tools/net40/IntelliFactory.WebSharper.Web.dll"
                ]
            [
                r.Assembly("System.Configuration")
                r.Assembly("System.Web")
                r.NuGet("Microsoft.AspNet.WebApi.Core")
                    .Version("5.1.0")
                    .Reference()
                r.NuGet("WebSharper").At(wsPaths).Reference()
            ])

bt.Solution [
    main
    bt.NuGet.NuSpec("WebSharper.WebApi.nuspec")
]
|> bt.Dispatch
