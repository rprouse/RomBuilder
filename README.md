# ROM Builder

ROM Builder merges several hex files into a single ROM file ready to be burned to an EPROM or extracts images out of a ROM file.

## Installation

This program is a [dotnet tool](https://docs.microsoft.com/en-us/dotnet/core/tools/global-tools) and requires the latest version of
the [.NET SDK](https://dotnet.microsoft.com/download) to be installed. .NET 5.0 or newer is recommended.

### Testing Locally

Build it, then package it using the **Pack** command in Visual Studio or `dotnet pack`
on the command line. Until this package is published, install it using the following
command line from the solution root;

```sh
dotnet tool install -g --add-source ./RomBuilder/nupkg dotnet-rombuilder
```

To update from a previous version,

```sh
dotnet tool update -g --add-source ./RomBuilder/nupkg dotnet-rombuilder
```

### Installing from GitHub Packages

Whenever the version is updated in `RomBuilder/RomBuilder.csproj`, a merge to master will publish the NuGet package
to [GitHub Packages](https://github.com/rprouse?tab=packages). You can install or update from there.

First you must update your global NuGet configuration to add the package registry and include the GitHub Personal
Access Token (PAT). This file is in `%appdata%\NuGet\NuGet.Config` on Windows and in `~/.config/NuGet/NuGet.Config`
or `~/.nuget/NuGet/NuGet.Config` on Linux/Mac.

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <packageSources>
    <add key="nuget.org" value="https://api.nuget.org/v3/index.json" protocolVersion="3" />
    <add key="Local" value="C:\temp" />
    <add key="Microsoft Visual Studio Offline Packages" value="C:\Program Files (x86)\Microsoft SDKs\NuGetPackages\" />
    <add key="github" value="https://nuget.pkg.github.com/rprouse/index.json" />
  </packageSources>
  <packageSourceCredentials>
    <github>
      <add key="Username" value="rprouse" />
      <add key="ClearTextPassword" value="GITHUB_PAT" />
    </github>
  </packageSourceCredentials>
</configuration>
```

Once that is done, to install,

```sh
dotnet tool install -g dotnet-rombuilder
```

And to update from a previous version,

```sh
dotnet tool update -g dotnet-rombuilder
```

## Enabling Tab Completion

This program supports tab completion using `dotnet-suggest`. To enable, for each shell
you must install the `dotnet-suggest` global tool and adding a shim to your profile. This
only needs to be done once and work for all applications built using `System.CommandLine`.

Follow the [setup instructions](https://github.com/dotnet/command-line-api/blob/main/docs/dotnet-suggest.md)
for your shell.
