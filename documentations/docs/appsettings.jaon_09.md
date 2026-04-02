# appsettings.json

In previous versions of ASP.NET, we store application configuration settings, like, for example, database connection strings in **web.config** file.

In ASP.NET Core application configuration settings can come from the following different configurations sources:
- **Files**: (*appsettings.json*, *appsettings.**\{Environment\}**.json*, where *\{Environment\}* is the api's current hosting environment)
- **User secrets**
- **Environment variables**
- **Command-line arguments**

**The latest configuration source overwrite previous configuration source. If same *Key* exists in both *appsettings.json* and as an *Envionment Varaible*, the *Environment Variable* orerwrites the one in *appsettings.json* file**

The **appsettings.json**, (or **appsettings.***development***.json**) were created during the project cteation.

- In this file we can set new configurations for our application, It is set in a *key, value* pairs in a **JSON** format. Let's add, for example, new key named ***MyKey***:
    ```json
    {
    "Logging": {
        "LogLevel": {
        "Default": "Information",
        "Microsoft.AspNetCore": "Warning"
        }
    },
    "AllowedHosts": "*",

    "MyKey": "Value of MyKey from appsettings.json"
    }
    ```
    - To access configuration information, we need access to ***IConfiguration service*** provided by the framework. We can get it in several ways two of them are:
    - Read configuration directly from the builder (the simplest way)
        ```c#
        _config = builder.Configuration;
        ```
        while keeping the *_config* variable as a private memeber in the class
    - Let DI inject I_Configuration into a minimal API handler
      ```c#
      app.MapGet("/", (IConfiguration config) => {
          string? configValue = config["MyKey"];
          return configValue;
      });
      ```
        - Note: If you want to use ```IConfiguration``` in a non-minimal API handler, we can also register it as a service:
      ```builder.Services.AddSingleton<IConfiguration>(builder.Configuration);``` Then we can inject IConfiguration into any class constructor
        - In this case Since IConfiguration is registered as a service by default, we can directly inject it into the minimal API handler without needing to register it explicitly
        - IConfiguration service is registered as a singletone, so in any request we will get the same instance, (so we don't need to keep it in this class)
        - Note: If you want to read configuration in a non-minimal API handler, you can also use the static property ConfigurationManager.AppSettings["MyKey"] to read configuration values without needing to inject IConfiguration  
