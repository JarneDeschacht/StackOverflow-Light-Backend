using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackOverflowLight_api.Data;
using StackOverflowLight_api.Data.Repositories;
using StackOverflowLight_api.Models;
using System;

namespace StackOverflowLight_api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            //var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");
            var connectionString = "Server=.\\sqlexpress;Database=stackoverflowdb;Trusted_Connection=True";
            services.AddDbContext<StackOverflowContext>(options => options.UseSqlServer(connectionString));

            services.AddOpenApiDocument(c => {
                c.DocumentName = "apidocs";
                c.Title = "stackoverflowAPI";
                c.Version = "v1";
                c.Description = "documentation for stackoverflowAPI";
            });

            services.AddScoped<DataInitializer>();
            services.AddScoped<IUserRepository, UserRepository>();
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,DataInitializer stackoverflowdatainitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwaggerUi3();
            app.UseSwagger();
            stackoverflowdatainitializer.InitializeData().Wait();
        }
    }
}
