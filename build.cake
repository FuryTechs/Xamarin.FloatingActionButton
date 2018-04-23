var target = Argument("target", "Default");
var local = BuildSystem.IsLocalBuild;
var appName = "FuryTechs.FloatingActionButton";
var versionParam = Argument<string>("BuildVersion");
var outputDirectory = Argument<string>("OutputDirectory");
var versionParts = versionParam.Split('.');

var version = string.Format("{0}.{1}.{2}", versionParts[0],versionParts[1],versionParts[2]);
var semVersion = local ? version : (version + string.Concat("-build-", versionParts[3]));
var configuration = Argument("configuration", "Release");
var primaryAuthor = "FuryTechs";

var touchDir = MakeAbsolute(Directory("./build-artifacts/output/touch"));
var droidDir = MakeAbsolute(Directory("./build-artifacts/output/droid"));
var coreDir  = MakeAbsolute(Directory("./build-artifacts/output/core"));
Setup(context =>
{
    var binsToClean = GetDirectories("./src/**/bin/");
 var artifactsToClean = new []{
        touchDir.ToString(), 
        droidDir.ToString(), 
        coreDir.ToString()
 };
 CleanDirectories(binsToClean);
 CleanDirectories(artifactsToClean);

    //Executed BEFORE the first task.
    Information("Building version {0} of {1}.", semVersion, appName);
});

Task("Default")
  .IsDependentOn("Package Library");

Task("Build Droid")
  .IsDependentOn("Patch Assembly Info")
  .Does(() =>
{
  MSBuild("./src/FuryTechs.FloatingActionButton.Android/FuryTechs.FloatingActionButton.Android.csproj", new MSBuildSettings
      {
       ToolVersion = MSBuildToolVersion.VS2017
   }
      .WithProperty("OutDir", droidDir.ToString())
      .SetConfiguration(configuration));
});

Task("Build Touch")
  .IsDependentOn("Patch Assembly Info")
  .Does(() =>
{
  MSBuild("./src/FuryTechs.FloatingActionButton.iOS/FuryTechs.FloatingActionButton.iOS.csproj", new MSBuildSettings
      {
       ToolVersion = MSBuildToolVersion.VS2017,
     MSBuildPlatform = (Cake.Common.Tools.MSBuild.MSBuildPlatform)1
   }
      .WithProperty("OutDir", touchDir.ToString())
      .SetConfiguration(configuration));
});

Task("Build Core")
  .IsDependentOn("Patch Assembly Info")
  .Does(() =>
{
  MSBuild("./src/FuryTechs.FloatingActionButton/FuryTechs.FloatingActionButton.csproj", new MSBuildSettings
      {
       ToolVersion = MSBuildToolVersion.VS2017
   }
      .WithProperty("OutDir", coreDir.ToString())
      .SetConfiguration(configuration));
});

Task("Patch Assembly Info")
    .Does(() =>
{
    var file = "./src/SolutionInfo.cs";

    CreateAssemblyInfo(file, new AssemblyInfoSettings
    {
        Product = appName,
        Version = version,
        FileVersion = version,
        InformationalVersion = semVersion,
        Copyright = "Copyright (c) " + DateTime.Now.Year.ToString() + " " + primaryAuthor
    });
});

Task("Package Library")
  .IsDependentOn("Build Droid")
  .IsDependentOn("Build Touch")
  .IsDependentOn("Build Core")
  .Does(() => {
    var nuGetPackSettings   = new NuGetPackSettings {
                                    Id                      = appName,
                                    Version                 = version,
                                    Title                   = "Xamarin Forms Floating Action Button",
                                    Authors                 = new[] {primaryAuthor},
                                    Description             = "Xamarin Forms Floating Action Button",
                                    ProjectUrl              = new Uri("https://github.com/FuryTechs/Xamarin.FloatingActionButton"),
                                    Files                   = new [] {
                                                                        new NuSpecContent {Source = coreDir.ToString() + "/FuryTechs.FloatingActionButton.dll", Target = "lib/netcore45"},
                                                                        new NuSpecContent {Source = coreDir.ToString() + "/FuryTechs.FloatingActionButton.dll", Target = "lib/netstandard2.0"},
                                                                        new NuSpecContent {Source = coreDir.ToString() + "/FuryTechs.FloatingActionButton.dll", Target = "lib/portable-net45+win8+wpa81+wp8"},

                                                                        new NuSpecContent {Source = droidDir.ToString() + "/FuryTechs.FloatingActionButton.Android.dll", Target = "lib/MonoAndroid"},
                                                                        new NuSpecContent {Source = droidDir.ToString() + "/FuryTechs.FloatingActionButton.dll", Target = "lib/MonoAndroid"},

                                                                        new NuSpecContent {Source = touchDir.ToString() + "/FuryTechs.FloatingActionButton.iOS.dll", Target = "lib/Xamarin.iOS10"},
                                                                        new NuSpecContent {Source = touchDir.ToString() + "/FuryTechs.FloatingActionButton.dll", Target = "lib/Xamarin.iOS10"},
                                                                      },
                                    Dependencies            = new [] { 
                                                                        new NuSpecDependency { Id = "Xamarin.Forms", Version  = "2.5" }
                                                                      },
                                    BasePath                = "./src",
                                    NoPackageAnalysis       = true,
                                    OutputDirectory         = outputDirectory
                                };

    NuGetPack(nuGetPackSettings);
  });

RunTarget(target);