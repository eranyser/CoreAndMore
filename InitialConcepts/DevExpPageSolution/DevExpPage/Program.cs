namespace DevExpPage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            //app.UseDefaultFiles();
            //app.UseStaticFiles();
            app.UseFileServer();

            app.Run(async context =>
            {

                Console.WriteLine("I am in the app.Run middleware");
                Console.WriteLine($"--> {context.Request.Method} {context.Request.Path}");
                throw new Exception($"This is an exception from middleware! {context.Request.Method} {context.Request.Path}");
                await context.Response.WriteAsync("Hello from middlware!");
            });

            app.Run();
        }
    }
}
