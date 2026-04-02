# Environment Variables

In ASP.NET Core you generally have two good ways to read an environment variable value.

### Recommended: via IConfiguration (works with env vars + appsettings)

In minimal hosting:
```c#
var value = builder.Configuration["MyEnvVarName"];
// or
var value2 = builder.Configuration.GetValue<string>("MyEnvVarName");
```
If the env var is a key like [MySettings__ApiKey (note the __)](./env_variables_13_1.md), you can bind it under sections too.

### Directly: ```Environment.GetEnvironmentVariable```

This reads only the OS environment variable:
```c#
var value = Environment.GetEnvironmentVariable("MyEnvVarName");
```

### Common example

To read teh environment name itself:
```c#
var envName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
```

**Out of the box, ASP.NET Core provides:**
- **IHostingEnvironmetn** (older, web-host specific / legacy naming)
    - Older ASP.NET Core interface name (from the time when the concept was split between “web host” and “generic host”).
    - In modern code it’s effectively replaced by IWebHostEnvironment.
    - Typically includes web-specific info like WebRootPath (depending on version).
- **IHostEnvironment** (newer, generic host)
    - Lives in Microsoft.Extensions.Hosting
    - Works for any .NET host type (web or non-web).
    - Lets you check things like:
        - EnvironmentName (Development, Production, etc.)
        - ContentRootPath
        - IsDevelopment(), IsProduction() …

### Best practice
 - If you only need dev/prod checks and content root: inject IHostEnvironment.
 - If you need web-specific paths (like WebRootPath for wwwroot): inject IWebHostEnvironment.
 - IHostingEnvironment is an older legacy interface. In modern ASP.NET Core code, you typically **don’t use it**, you use IHostEnvironment (or IWebHostEnvironment).

### IConfiguration vs IHostEnvironment / IWebHostEnvironment (HostingEnvironment)

They both relate to “environment variables,” but they solve different problems.

#### IConfiguration (Configuration services)
IConfiguration is a general key/value system. It can read from many sources:
    - appsettings.json
    - appsettings.\{Environment\}.json
    - environment variables
    - command-line args
    - user secrets (dev)
So when you do:
    - configuration["MySetting"]
    - or bind to options you might be getting the value from any of those sources, with a precedence order. Environment variables are just one possible provider.

#### IHostEnvironment / IWebHostEnvironment (HostingEnvironment)

This service is not for reading arbitrary env vars like MY_API_KEY.

It tells you about the hosting environment of the app, mainly:
- EnvironmentName (e.g., Development, Production)
- IsDevelopment(), IsProduction()
- paths like ContentRootPath, WebRootPath

It’s used for conditional behavior, like:
- “show dev exception page only in Development”
- “use dev-only settings”

### Best way to access environment variables

For “real settings” (recommended)
Use IConfiguration (or options binding):
```C#
var value = builder.Configuration["SomeEnvVarName"];
```
Or bind strongly-typed options:
```C#
builder.Services.Configure<MySettings>(
    builder.Configuration.GetSection("MySettings"));
```
Then inject ```IOptions<MySettings>``` where needed.

This is best because it works whether the value comes from env vars or JSON, and it’s testable.

#### For environment name / dev-vs-prod checks
Inject IHostEnvironment (or use app.Environment):
```c#
if (app.Environment.IsDevelopment())
{
    // dev-only behavior
}
```
#### For direct env var lookup (simple/edge cases)
Use .NET directly:
```c#
var v = Environment.GetEnvironmentVariable("SomeEnvVarName");
```
This bypasses IConfiguration layering (and you’ll handle precedence yourself), so it’s less common in ASP.NET Core apps.

### Summary
- Use IConfiguration when you want configuration values (including those coming from env vars).
- Use IHostEnvironment when you want to know whether the app is running as Development/Production and pick behavior accordingly.
- Use Environment.GetEnvironmentVariable only for quick direct reads.

**Bibliography:** 
**Old Way**
1. [ASP NET Core environment variables](https://www.youtube.com/watch?v=x8jNX1nb_og&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&index=14)

    [![](https://i.ytimg.com/vi/x8jNX1nb_og/hqdefault.jpg?sqp=-oaymwEmCKgBEF5IWvKriqkDGQgBFQAAiEIYAdgBAeIBCggYEAIYBjgBQAE=&rs=AOn4CLB-O6_owALCQzCrJeLjwrC4YojxrA)](https://www.youtube.com/watch?v=x8jNX1nb_og&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&index=14)

