# Double Underscore, __, in Environment Variables

In environment variables, the __ (double underscore) is how ASP.NET Core represents a nested “section” key.

ASP.NET Core follows this convention:

- MySettings__ApiKey means “section MySettings, key ApiKey”
- So it maps to what you’d normally write in appsettings.json like:
```json
{
  "MySettings": {
    "ApiKey": "value-here"
  }
}
```
So when you do:
- builder.Configuration["MySettings:ApiKey"]
you can also supply it via env var:
- MySettings__ApiKey

### Example (bind options)

If you have:
```json
"MySettings": { "ApiKey": "..." }
```
You can set:

 - env var: MySettings__ApiKey=123

and bind MySettings using 
```c# 
builder.Services.Configure<MySettings>(...).
```
: isn’t allowed in environment variable names on many systems, so __ is the standard workaround.

