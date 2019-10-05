using System;
using AspNetCore.RouteAnalyzer;
using CRM.DAL.Database;
using CRM.DAL.Database.Entities.Customer;
using CRM.DAL.Database.Entities.Employee;
using CRM.Dto.Settings;
using CRM.WebAPI.Infrastructure;
using CRM.WebAPI.Middlewares;
using CRM.WebAPI.Middlewares.AuthenticatedUser;
using CRM.WebAPI.ODataModelBuilder;
using Elmah.Io.AspNetCore;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace CRM.WebAPI
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
            BootstrapService.Initialize(services, Configuration);
            services.AddDbContext<CRMContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CRM")));
            services.AddMvc()
                .AddJsonOptions(
                    options =>
                    {
                        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                        if (options.SerializerSettings.ContractResolver != null)
                        {
                            var resolver = options.SerializerSettings.ContractResolver as DefaultContractResolver;
                            resolver.NamingStrategy = null;
                        }
                    }


                );
            services.Configure<HttpClientSettings>(Configuration.GetSection("HttpClientSettings"));
            services.AddOData();
            services.AddTransient<CRMModelBuilder>();

            var elmahApiKey = Configuration.GetSection("ElmahIo:ApiKey").Value;
            var elmahLogId = Configuration.GetSection("ElmahIo:LogId").Value;

            services.AddElmahIo(o =>
            {
                o.ApiKey = elmahApiKey;
                o.LogId = Guid.Parse(elmahLogId);
                o.WebProxy = new ProxyBuilder(Configuration).Build();
            });
            services.AddRouteAnalyzer(); // Add
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseElmahIo();
            }
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseAuthenticatedDatabaseAccess();
            app.UseCors(entity => entity.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
            var builder = new CRMModelBuilder(app.ApplicationServices);
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.MapODataServiceRoute("ODataRoutes", "odata", builder.GetEdmModel());
                routeBuilder.EnableDependencyInjection();
                routeBuilder.MapRouteAnalyzer("/routes"); // Add
            });
        }
        private static void ConfigureModelToSupportOdata(ODataConventionModelBuilder builder)
        {
            
        }
    }
}
