namespace StaticFiles
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");

            /* The default file names are: index.html, default.html, and default.htm. You can change the default file names by using the DefaultFilesOptions class.
             * 
            DefaultFilesOptions options = new DefaultFilesOptions();
            options.DefaultFileNames.Clear();
            options.DefaultFileNames.Add("foo.html");
            app.UseDefaultFiles(options);
            *
            */
            app.UseDefaultFiles();
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
    }
}
