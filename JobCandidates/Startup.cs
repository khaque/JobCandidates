using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using JobCandidates.Core.Business_Interfaces;
using JobCandidates.Core.Utility_Interfaces;
using JobCandidates.Business;
using System.Reflection;
using System.IO;
using Microsoft.OpenApi.Models;

namespace JobCandidates
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<ICandidateService, CandidateService>();
            services.AddTransient<IJobService, JobService>();
            services.AddTransient<IFindCandidateService, FindCandidateService>();
            services.AddTransient<IServiceRequestHelper, ServiceRequestHelper>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "JobAdder API", Version = "v1" });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(
             options => options.WithOrigins("http://localhost:4200")
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .AllowCredentials());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSwagger(a =>
            {
                a.RouteTemplate = "jobcandidates/{documentName}/swagger.json";
            });

            app.UseSwaggerUI(a => {
                a.SwaggerEndpoint("/jobcandidates/v1/swagger.json", "jobAdder API v1");
                a.RoutePrefix = "swagger/ui";
            });

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
