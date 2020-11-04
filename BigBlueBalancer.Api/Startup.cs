using BigBlueButton.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Net.Http;

namespace BigBlueBalancer.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BigBlueBalancer.Api", Version = "v1" });
            });

            services.AddHttpClient("BigBlueButton", client =>
            {
                client.BaseAddress = new Uri("https://montclair-confs.artere.ma/bigbluebutton/api/");
            });
            services.AddScoped<IBBBClient, BBBClient>(sp =>
            {
                var httpClient = sp.GetRequiredService<IHttpClientFactory>().CreateClient("BigBlueButton");
                var logger = sp.GetRequiredService<ILogger<BBBClient>>();
                return new BBBClient(httpClient, "U5oVXerwGeAn7zVZ1E5J2HlvBj7HqEqzaozy3h9jQ", logger);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BigBlueBalancer.Api v1"));
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
