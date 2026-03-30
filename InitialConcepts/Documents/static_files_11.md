# Static Files

- By default, an asp.net core application will not serve static files
- The default directory for static files is ***wwwroot*** and this directory must be in the root project folder
- In order to serve static files the request processing pipeline must have the required middleware that can serve the Static Files.
- We will add the *UseStaticFiles()* middleware to our application's request processing pipeline as shown below.
    ```c#
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.UseStaticFiles();
        
        app.Run();
    }
    ```
- Now lets add the *wwwroot* folder with the folowing files:
    ```
    📁 StaticFiles
    └──📂 wwwroot
        └──📂 images
            ├── RedCar.jpg
            ├── YellowCar.jpg
            ├── GrayCar.jpg
            └── GreenCar.jpg
    ```

    We can navigate to each file via the URL: ```https://localhost:<port>/images/RedCar.png```

- Most web applications have a default document/file which displayed when a user visits the root URL of your application
- To be able to serve default file, (like *default.html*), we have to plug in the ***UseDefaultFiles()*** middleware in our application's request processing pipeline.
- UseDefaultFiles must be called before UseStaticFiles to serve the default file. 
- The following are the default files which UseDefaultFiles middleware looks for:
    - index.htm
    - index.html
    - default.htm
    - default.html
- If we want to use another document like ***foo.html*** as the default, we can use the *Option* class and supply it to the middleware:
    ```C#
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("foo.html");

            app.UseDefaultFiles(options);
            app.UseStaticFiles();

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
- UseFileServer combines the functionality of UseStaticFiles, UseDefaultFiles and UseDirectoryBrowser middleware. 
- DirectoryBrowser middleware, enables directory browsing and allows users to see files within a specified directory. We could replace UseStaticFiles and UseDefaultFiles middlewares with UseFileServer Middleware.
- The pattern to add middleware is as follows:
    - In most cases we add middleware using the extension methods that start with the word USE
    - If we want to customize these middleware components, we use the respective OPTIONS object
    
    | Middleware | Options Object |
    |------------|----------------|
    | UseDeveloperExceptionPage | DeveloperExceptionPageOptions | 
    | UseDefaultFiles | DefaultFilesOptions |
    | UseStaticFiles | StaticFileOptions |
    | UseFileServer | FileServerOptions |

**Bibliography:** 

1. [Static Files in ASP.NET Core](https://www.youtube.com/watch?v=yt6bzZoovgM&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&index=12)

    [![](https://i.ytimg.com/vi/yt6bzZoovgM/hqdefault.jpg?sqp=-oaymwEmCKgBEF5IWvKriqkDGQgBFQAAiEIYAdgBAeIBCggYEAIYBjgBQAE=&rs=AOn4CLCbGlGXiyOdhf-3tsg96XAaaJSBFg)](https://www.youtube.com/watch?v=yt6bzZoovgM&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&index=12)

| | | |
|-|-|-|
[![Home](../../Documents/Images/home_button.svg)](../../README.md) | [![Previous](../../Documents/Images/back_button.svg)](./middleware_10.md) | [![Next](../../Documents/Images/next_button.svg)](./dev_exp_page_12.md) |
| | | 