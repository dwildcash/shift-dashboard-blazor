using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.StaticWebAssets;
using Microsoft.Extensions.Hosting;

namespace shift_dashboard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://localhost:6000"); // Configure on port 6000 instead of default
                    webBuilder.UseStartup<Startup>();

                    // 👇 Add this "ConfigureAppConfiguration" method calling.
                    webBuilder.ConfigureAppConfiguration((ctx, cb) =>
                    {
                        // 👇 This call inserts "StaticWebAssetsFileProvider" into
                        //    the static file middleware.
                        StaticWebAssetsLoader.UseStaticWebAssets(
                          ctx.HostingEnvironment,
                          ctx.Configuration);
                    });
                });
    }
}