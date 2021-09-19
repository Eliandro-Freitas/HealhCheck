using HealCheck.Infra;
using HealCheck.Infra.Repositories;
using HealhCheck.Domain.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HealhCheck.Api
{
    public class Startup
    {
        const string _connectionString = "Data Source=localhost;Initial Catalog=Estudos;User Id=sa;Password=@Elifreitas0;";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddHealthChecks()
                .AddSqlServer(_connectionString, name: "Instancia SQL Server");

            //services.AddDbContext<DataContext>(x => x.UseSqlServer(_connectionString));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HealhCheck.Api", Version = "v1" });
            });

            services.AddSingleton<ICompanyRepository, CompanyRepository>();
            services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(1);
                options.MaximumHistoryEntriesPerEndpoint(10);
                options.AddHealthCheckEndpoint("API com Health Checks", "/health");
            })
            .AddInMemoryStorage();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HealhCheck.Api v1"));
            }

            app.UseHealthChecks("/health", new HealthCheckOptions
            {
                Predicate = p => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options => { 
                options.UIPath = "/dashboard"; 
                options.AddCustomStylesheet(@"C:\Estudos\HealhCheck\HealhCheck\HealhCheck.Api\CustomDashboard\dashboardCustom.css");
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
