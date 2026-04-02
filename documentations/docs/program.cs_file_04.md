# The Main method in *Program.cs*

ASP.NET core application initially starts as a console application and the Main() method in Program.cs file is the entry point. 

- ```var builder = WebApplication.CreateBuilder(args);```
    - Creates a WebApplicationBuilder object that is used to configure services and middleware for the application, such as: ***logging, dependency injection, Kestrel*** etc. The args parameter contains command-line arguments passed to the application.
- ```var app = builder.Build();```
    - Builds and creates the WebApplication instance, (this is actually the host that hosts our ASP.NET Core Web application), based on all the services and configurations that were registered with the builder. This composes the service provider and creates the middleware/endpoint infrastructure used to handle requests.
- ```app.MapGet("/", () => System.Diagnostics.Process.GetCurrentProcess().ProcessName);``` Registers a minimal-API endpoint for HTTP GET at path /. The lambda is the request handler and returns a string, the current process name. Under the hood ASP.NET Core wires this into the routing/endpoint system so a GET to / invokes this handler.
- ```app.Run();``` - Starts the web server and begins listening for incoming HTTP requests. This is a blocking call that keeps the application running, (it runs the server loop until shutdown).

### Notes / behavior summary
 - MapGet is provided by ASP.NET Core and extends WebApplication. 
    - It registers an HTTP GET endpoint in the application's routing system.
    - 
 - MapGet only configures the handler; ```Run()``` actually starts the server.
 - The handler return value (string) is written to the response body with appropriate headers by the framework.
 - CreateBuilder + Build apply many framework defaults (routing middleware, DI, logging, configuration) so you don't have to call ```UseRouting()``` or similar manually for this minimal setup.

