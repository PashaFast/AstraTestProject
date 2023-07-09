using AstraTestProject.Data.AstraTestProjectDb;
using AstraTestProject.Extensions;
using AstraTestProject.Services.HomeService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
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
using AstraTestProject.SignalR;

namespace AstraTestProject
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
            services.AddDbContext<AstraTestProjectContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("AstraTestProjectDbConnection")));

            ConfigureDIService(services);
            ConfigureAutoMapperService(services);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AstraTestProject", Version = "v1" });
            });

            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AstraTestProject v1"));
            }
            app.ConfigureExceptionHandler();

            app.UseCors((options => options.AllowCredentials().AllowAnyHeader().AllowAnyMethod().WithOrigins(new string[] { "https://localhost:44348" })));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
                endpoints.MapControllers();

            });
        }

        private void ConfigureDIService(IServiceCollection services)
        {
            services.AddScoped<IHomeService, HomeService>();
        }

        private void ConfigureAutoMapperService(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
        }
    }
}
