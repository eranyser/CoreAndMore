using System.Runtime.CompilerServices;

namespace EmptyProject
{
    public class Program
    {
        private static IConfiguration? _config;
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();

            // Read configuration directly from the builder (the simplest way)
            // _config = builder.Configuration;

            // We can also let DI inject IConfiguration into a minimal API handler
            // Note: If you want to use IConfiguration in a non-minimal API handler, you can also register it as a service:
            // builder.Services.AddSingleton<IConfiguration>(builder.Configuration); Then you can inject IConfiguration into any class constructor
            // In this case Since IConfiguration is registered as a service by default, we can directly inject it into the minimal API handler without needing to register it explicitly
            // IConfiguration service is registered as a singletone, so in any request we will get the same instance, (so we don't need to keep it in this class)
            // Note: If you want to read configuration in a non-minimal API handler, you can also use the static property ConfigurationManager.AppSettings["MyKey"] to read configuration values without needing to inject IConfiguration
            app.MapGet("/", (IConfiguration config) => {
                string procName = System.Diagnostics.Process.GetCurrentProcess().ProcessName;
                string? configValue = config["MyKey"];
                return procName + '\n' + configValue;
            });
            
            app.Run();
        }
    }
}
