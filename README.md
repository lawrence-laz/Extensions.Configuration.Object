![Extensions.Configuration.Object Logo](https://raw.githubusercontent.com/lawrence-laz/Extensions.Configuration.Object/master/src/Extensions.Configuration.Object/header.png)

[![NuGet Version](https://img.shields.io/nuget/v/Extensions.Configuration.Object?label=NuGet)](https://www.nuget.org/packages/Extensions.Configuration.Object/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/Extensions.Configuration.Object?label=Downloads)](https://www.nuget.org/packages/Extensions.Configuration.Object/)
[![Build](https://github.com/lawrence-laz/Extensions.Configuration.Object/workflows/Build/badge.svg)](https://github.com/lawrence-laz/Extensions.Configuration.Object/actions?query=workflow%3ABuild)

# What does this package do?
It allows to load nested configuration from objects instead of loading from JSON or other methods. This is useful in the context of automated tests as the configuration can be defined inside the test itself instead of having multiple JSON files.

In other words you can do this:
```csharp
var configuration = new ConfigurationBuilder()
    .AddObject(new
    {
        MyProperty = "MyValue",
        MySection = new 
        {
            MyOtherProperty = "MyOtherValue"
        }
    })
    .Build();
```

Instead of this:
```csharp
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();
```
```json
// appsettings.json
{
    "MyProperty": "MyValue",
    "MySection": { 
        "MyOtherProperty": "MyOtherValue"
    }
}
```

# How to get started?
Download from [nuget.org](https://www.nuget.org/packages/Extensions.Configuration.Object/):
```powershell
PS> Install-Package Extensions.Configuration.Object
```

Look for [examples in test project](https://github.com/lawrence-laz/Extensions.Configuration.Object/tree/master/test/Extensionsions.Configuration.Object.UnitTests).
