# In Process Hosting Model

An ASP.NET core application can be hosted:

- InProcess - Inside iis/iisexpress process
- OutOfProcess - As a standalone process using **Kestrel** server

## InProcess Hosting

To configure *InProcess* hosting, **(the default configuration)**, we set in the porject file the following configuration:
```html
<PropertyGroup>
  <AspNetCoreHostingModel>InProcess</AspNetCoreHostingModel>
</PropertyGroup>
```
As can be read [***Here - In-process hosting with IIS and ASP.NET Core***](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/in-process-hosting?view=aspnetcore-10.0), In process hosting model means the *CreateDefaultBuilder* adds an IServer instance by calling the ***UseIIS()*** method to boot the CoreCLR and host the app inside of the IIS worker process (w3wp.exe or iisexpress.exe).

With InProcess hosting, there is only one web server and that is the IIS server that hosts our application.

![InProcessHosting](./images/InProcessHosting.png)

- From a performance point of view, InProcess hosting delivers higher request throughput than OutOfProcess hosting
- In the case of IIS, the process name that executes the app is w3wp and in the case of IIS Express it is iisexpress
- To get the process name executing the app, use ```System.Diagnostics.Process.GetCurrentProcess().ProcessName```
- When we are run the project from Visual Studio it uses IISExpress by default.

