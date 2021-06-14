using GlassLewisTest.API.Infrastructure;
using GlassLewisTest.API.Infrastructure.Middleware;
using GlassLewisTest.Application.Services;
using GlassLewisTest.Application.Services.Interfaces;
using GlassLewisTest.DataAccess.Infrastructure;
using GlassLewisTest.DataAccess.Repositories;
using GlassLewisTest.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GlassLewisTest.API
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
            services.AddOptions();
            services.AddLogging();
            services.AddControllers();
            services.AddHealthChecks();
            services.ConfigureServicesSwagger();

            services.Configure<ConnectionOptions>(Configuration.GetSection(nameof(ConnectionOptions)));
            services.Configure<AuthenticationOptions>(Configuration.GetSection(nameof(AuthenticationOptions)));

            services.AddTransient<IExchangesRepository, ExchangesRepository>();
            services.AddTransient<ICompaniesRepository, CompaniesRepository>();
            services.AddTransient<ICompaniesService, CompaniesService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureSwagger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GlassLewisTest.API v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseMiddleware<PerformanceMiddleware>();
            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
