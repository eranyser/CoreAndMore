# launchsettings.json

The **launchsettings.json** file resides in the "Properties" folder in the project root folder.

This file is only used on local development machine. We do not need it for publishing our asp.net core application.

If there are certain settings that you want your ASP.NET Core application to use when you publish and deploy your app, store them in **appsettings.json** file. We usually store our application configuration settings in this file, resides in the root directory.

The **launchsettings.json** actually set the **running profiles** of the application when running  it from local machine.

```json
{
  "profiles": {
    "http": {
      "commandName": "Project",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "dotnetRunMessages": true,
      "applicationUrl": "http://localhost:5295"
    },
    "https": {
      "commandName": "Project",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "dotnetRunMessages": true,
      "applicationUrl": "https://localhost:7154;http://localhost:5295"
    },
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    }
  },
  "$schema": "https://json.schemastore.org/launchsettings.json",
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:50708/",
      "sslPort": 44383
    }
  }
}
```
