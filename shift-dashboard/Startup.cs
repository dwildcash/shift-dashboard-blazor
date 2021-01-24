using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using Quartz;
using shift_dashboard.Data;
using shift_dashboard.Jobs;
using shift_dashboard.Model;
using shift_dashboard.Services;
using System;
using System.Linq;
using System.Net.Http;

namespace shift_dashboard
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            // Bind the Appsettings.json to shiftDashboardConfig
            DashboardConfig shiftDashboardConfig = new DashboardConfig();
            Configuration.GetSection(shiftDashboardConfig.Position).Bind(shiftDashboardConfig);
            services.AddSingleton<DashboardConfig>(shiftDashboardConfig);

            // Initialize DB Context
            services.AddDbContext<DashboardContext>(options =>
            {
                var builder = new NpgsqlDbContextOptionsBuilder(options);
                builder.SetPostgresVersion(new Version(9, 6));
                options.UseNpgsql(shiftDashboardConfig.ConnectionString);
            }
            );

            // Shift Api Service (Need a DB Context
            services.AddTransient<IApiService, ApiService>();

            // Schedule Tasks.
            services.AddQuartz(q =>
            {
                // Create a "key" for the job
                var updateDelegateJobKey = new JobKey("UpdateDelegateJob");

                // Register the job with the DI container
                q.AddJob<UpdateDelegateJob>(opts => opts.WithIdentity(updateDelegateJobKey));

                // Create a trigger for the job
                //
                q.AddTrigger(opts => opts
                .ForJob(updateDelegateJobKey)
                .WithIdentity("UpdateDelegateJob-trigger") // give the trigger a unique name
                .WithCronSchedule("0 */45 * * * ?")); ; // run every 45 minutes

                // Use a Scoped container to create jobs. I'll touch on this later
                q.UseMicrosoftDependencyInjectionScopedJobFactory();
            });

            // Add the Quartz.NET hosted service

            services.AddQuartzHostedService(
                q => q.WaitForJobsToComplete = true);

            if (services.All(x => x.ServiceType != typeof(HttpClient)))
            {
                services.AddScoped(
                    s =>
                    {
                        var navigationManager = s.GetRequiredService<NavigationManager>();
                        return new HttpClient
                        {
                            BaseAddress = new Uri(navigationManager.BaseUri)
                        };
                    });
            }

            services.AddBlazorise(options => { options.ChangeTextOnKeyPress = true; });
            services.AddBootstrapProviders();
            services.AddFontAwesomeIcons();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.ApplicationServices
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}