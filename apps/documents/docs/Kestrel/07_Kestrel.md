# Kestrel Web Server

**[Kestrel](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/servers/kestrel?view=aspnetcore-10.0) is a cross-platform web server** that is embedded in any ASP.NET Core application. 

It is supported on all platforms and versions of .NET Core. It is included by default as internal server in ASP.NET Core. 

Kestrel can be used, by itself as an edge server i.e Internet-facing web server that can directly process the incoming HTTP requests from the client. In Kestrel, the process used to host the app is dotnet.exe.

When we run a .NET Core application using the .NET Core CLI (Command-Line Interface), the application uses Kestrel as the web server. 

With ***OutOfProcess*** Hosting model, Kestrel can be used in one of the following 2 ways:
- [Stand-alone Web Server](./07_01_kestrel_stand_alone.md).
- [Together with a *Reverse Proxy* server](./07_02_Kestrel_with_reverse_proxy.md).


