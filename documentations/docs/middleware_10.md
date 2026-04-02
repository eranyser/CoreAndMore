# Middleware in ASP.NET Core

Middleware is a piece of software that can handle an HTTP request or response. A given middleware component in ASP.NET Core has a very specific purpose, for example:
- Component that authenticates a user.
- Middleware to handle errors.
- Middleware to serve static files such as JavaScript files, CSS files, Images etc.

**Middleware components sets up the request processing pipeline in ASP.NET Core.**

The following code set up a simple request processing pipeline with only one middleware. The only thing the application can do is write a message to the response object that will be displayed in the browser.

```c#
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.Run(async context =>
        {
            await context.Response.WriteAsync("Hello form first middleware!");
        });

        app.Run();
    }
}
```

- Pay attention the two *app.Run()* methods the first on comes from [RunExtensions.Run](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.runextensions.run?view=aspnetcore-10.0), this is an extention method and the second comes from [WebApplication.Run](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.builder.webapplication.run?view=aspnetcore-10.0) this is a method of *WebApplication* that runs the app and blocks the calling thread until host shutdown.
- With this *app.Run()* extension method we can add only a terminal middleware to the request pipeline. A terminal middleware is a middleware that does not call the next middleware in the pipeline
- If we want the middleware to call the next middleware in the pipeline, we do it by register the middleware with the *Use()* method:
    ```c#
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.Use(async (context, next) =>
        {
            await context.Response.WriteAsync("Hello from first middlware!\n");
            await next();
        });

        app.Run(async context =>
        {
            await context.Response.WriteAsync("Hello from second middlware!");
        });

        app.Run();
    }
    ```
    Notice the *Use()* method has two parameters
    - HttpContext - context object
    - Func type - a generic delegate that represents the next middleware in the pipeline.

The following diagram lets us understand middleware components and how they fit in a request processing pipeline
```mermaid
flowchart LR
    A["Logging"] --> B["StaticFiles"] --> C["MVC"]-->B-->A

    style A fill:#e8f4ff,stroke:#1e88e5,stroke-width:2px,color:#0d47a1
    style B fill:#fff3e0,stroke:#fb8c00,stroke-width:2px,color:#e65100
    style C fill:#e8f5e9,stroke:#43a047,stroke-width:2px,color:#1b5e20

```
In ASP.NET Core, a Middleware component has access to both - the incoming request and the outgoing response.

A Middleware component may process an incoming request and pass it to the next piece of middleware in the pipeline for further processing.

A middleware component may handle the request and decide not to call the next middleware in the pipeline. (This is called short-circuiting the request pipeline). For example, if the request is for a static file like an image or css file, the StaticFiles middleware can handle and serve that request and short-circuit the rest of the pipeline. This means in our case, the StaticFiles middleware will not call the MVC middleware if the request is for a static file.


**Bibliography:** 

1. [ASP.NET Core Middleware](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/middleware/?view=aspnetcore-10.0)
2. [Middleware in ASP.NET Core](https://www.youtube.com/watch?v=ALu4jtvjSYw&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&index=11)

    [![](https://i.ytimg.com/vi/ALu4jtvjSYw/hqdefault.jpg?sqp=-oaymwEmCKgBEF5IWvKriqkDGQgBFQAAiEIYAdgBAeIBCggYEAIYBjgBQAE=&rs=AOn4CLApZDYE9-jvUdT0urEsQSlplPyC6A)](https://www.youtube.com/watch?v=ALu4jtvjSYw&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&index=11)

3. [Configure ASP NET Core request processing pipeline](https://www.youtube.com/watch?v=nt6anXAwfYI&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&index=12)

    [![](https://i.ytimg.com/vi/nt6anXAwfYI/hqdefault.jpg?sqp=-oaymwEmCKgBEF5IWvKriqkDGQgBFQAAiEIYAdgBAeIBCggYEAIYBjgBQAE=&rs=AOn4CLCXYDtOfHjzqD61EtLkagGgV3P_wA)](https://www.youtube.com/watch?v=nt6anXAwfYI&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&index=12)

3. [HTL Perg: Mobile Computing and C# Course - Part 2 (ASP.NET Core Fundamentals)](https://www.youtube.com/watch?v=SpXNoqPJDwU&t=31m19s) from minute: 31:19

    [![](https://i.ytimg.com/vi/SpXNoqPJDwU/hqdefault.jpg?sqp=-oaymwEmCKgBEF5IWvKriqkDGQgBFQAAiEIYAdgBAeIBCggYEAIYBjgBQAE=&rs=AOn4CLC6CGkfTs3_W8AYPMDjn3OO583mIg)](https://www.youtube.com/watch?v=SpXNoqPJDwU&t=31m19s) 

