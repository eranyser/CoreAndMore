# Out of Process Hosting Model

As can be read [***here***](https://learn.microsoft.com/en-us/aspnet/core/host-and-deploy/iis/out-of-process-hosting?view=aspnetcore-10.0), to configure OutOfProcess hosting we set in the porject file the following configuration:
```html
<PropertyGroup>
  <AspNetCoreHostingModel>OutOfProcess</AspNetCoreHostingModel>
</PropertyGroup>
```
With out of forces hosting:
- There are 2 web servers - **An internal web server** and an **external web server**. 
- The internal web server is **Kestrel** and the external web server can be **IIS**, **Nginx** or **Apache**.
- With **InProcess hosting**, there is only **one web server** i.e the IIS that hosts the asp.net core application. 
- So, we do not have the performance penalty of proxying requests between internal and external web server.

    ![OutOfProcess](./images/OutOfProcessHosting.png)

