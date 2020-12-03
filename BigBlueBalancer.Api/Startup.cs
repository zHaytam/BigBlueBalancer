using AutoMapper;
using BigBlueBalancer.Api.Entities;
using BigBlueBalancer.Api.Filters;
using BigBlueBalancer.Api.Tasks;
using BigBlueButton.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;

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
            services.AddControllers(o =>
            {
                o.Filters.Add<ChecksumActionFilter>();
                o.Filters.Add<DecodeParamsFilter>();
            }).AddXmlSerializerFormatters();
            services.AddSwaggerGen(c =>
            {
                c.DescribeAllParametersInCamelCase();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BigBlueBalancer.Api", Version = "v1" });
            });

            services.AddHttpClient();
            services.AddDbContext<AppDbContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"),
                ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));
            services.AddScoped<IBBBClient, BBBClient>();
            services.AddAutoMapper(typeof(Startup));

            services.AddHostedService<ServersStatsBackgroundService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BigBlueBalancer.Api v1"));

            app.UseSerilogRequestLogging();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

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
