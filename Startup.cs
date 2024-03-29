using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BovensteVerdieping
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            // Load Funda Api Key from dotnet user-secrets
            var config = new AppConfiguration();
            config.FundaApiKey = Configuration["FundaApiKey"];
            services.AddSingleton<AppConfiguration>(config);
            // Inject Api Service
            services.AddSingleton<Services.ApiService>();
            // Inject Real Estate Service
            services.AddSingleton<Services.IRealEstateService, Services.RealEstateService>();
            services.AddSingleton<Services.SampleDataService>();
            // Inject Cache Service
            services.AddMemoryCache();
            services.AddSingleton<Services.CacheService>();
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
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
