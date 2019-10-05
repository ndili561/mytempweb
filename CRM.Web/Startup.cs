using CRM.Entity.Settings;
using CRM.Web.Helpers;
using Elmah.Io.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using CRM.Web.Middlewares;
using Microsoft.AspNetCore.Mvc;

namespace CRM.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            Bootstrap.Initialize(services, Configuration);
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));
            services.Configure<HttpClientSettings>(Configuration.GetSection("HttpClientSettings"));
            services.Configure<WebApplicationSettings>(Configuration.GetSection("WebApplicationSettings"));
            services.AddAntiforgery(o => o.HeaderName = "X-CSRF-TOKEN");
            services.AddMvc(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());//XSS Attack: Not required for GET as it does not mutate the resource
              //  options.Filters.Add(new RequireHttpsAttribute());// Force the website to run HTTPS  Identity does not work
            })
            ;

            var config = Configuration.GetSection("ElmahLogSettings").Get<ElmahLogSettings>();

            services.AddElmahIo(o =>
            {
                o.ApiKey = config.ApiKey;
                o.LogId = Guid.Parse(config.LogId);
                o.WebProxy = config.UseProxy 
                    ? new WebProxy(config.ProxyUri, false, new string[] { }, new NetworkCredential(config.ProxyUserName, config.ProxyPassword)) 
                    : null;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseElmahIo();
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();
            //app.UseAuthentication();
            app.UseWebSockets();
            app.UseMiddleware<ChatWebSocketMiddleware>();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
