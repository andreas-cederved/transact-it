using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerUI;
using TransactIt.Api.Filters;
using TransactIt.Infrastructure.Extensions;

namespace TransactIt.Api
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
            services.AddAutoMapperWithProfiles();
            services.AddDatabaseContexts(Configuration);
            services.AddMediatorWithRequests();
            services.AddControllers(options =>
            {
                options.Filters.Add(new ValidationExceptionFilter());
            }).AddFluentValidators();

            // Broken in current version of asp.net core preview.
            //services.AddApiVersioning(o =>
            //{
            //    o.ReportApiVersions = true;
            //    o.AssumeDefaultVersionWhenUnspecified = true;
            //    o.DefaultApiVersion = new ApiVersion(1, 0);
            //    o.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
            //});
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo
                {
                    Version = "v1.0",
                    Title = "TransactIt API",
                    Description = "TransactIt guide for API.",
                    Contact = new OpenApiContact
                    {
                        Name = "Andreas Cederved",
                        Email = "andreas.cederved@faultier.se"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "DBAD Public Licence",
                        Url = new Uri("https://dbad-license.org/")
                    }
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
            });

            //workaround bug in asp.net core 3 preview
            services.Add(
                new ServiceDescriptor(
                    typeof(IActionResultExecutor<JsonResult>),
                    Type.GetType("Microsoft.AspNetCore.Mvc.Infrastructure.SystemTextJsonResultExecutor, Microsoft.AspNetCore.Mvc.Core"),
                    ServiceLifetime.Singleton));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "TransactIt API V1.0");
                c.DocExpansion(DocExpansion.None);
            });

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
