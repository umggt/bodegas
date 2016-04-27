using Bodegas.Auth.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Bodegas.Auth
{
    public class Startup
    {
        private readonly IHostingEnvironment hosting;
        private readonly IApplicationEnvironment application;
        private readonly IConfigurationRoot configuration;

        public Startup(IApplicationEnvironment applicationEnvironment, IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            this.application = applicationEnvironment;
            this.hosting = hostingEnvironment;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{hosting.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            configuration = builder.Build();

            loggerFactory.AddSerilog();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var certificatePath = Path.Combine(application.ApplicationBasePath, "certificados", "idsrv4test.pfx");
            var certificate = new X509Certificate2(certificatePath, "idsrv3test");

            services.AddAuthenticationServer(certificate);
            services.AddMvc(Bodegas.Startup.ConfigureJsonFormatter).AddAuthenticationViewLocationExpander();

            services.AddTransient<LoginService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseIISPlatformHandler();

            app.UseCors(x => x.AllowAnyOrigin());

            app.UseStaticFiles();

            if (hosting.IsDevelopment())
            {
                var root = application.ApplicationBasePath;
                ConfigureDevelopmentEnvironment(app, root);
            }

            app.Map(new PathString("/auth"), config =>
            {
                config.UseIdentityServer();
                config.UseMvcWithDefaultRoute();
            });
        }

        private void ConfigureDevelopmentEnvironment(IApplicationBuilder app, string root)
        {
            app.UseDeveloperExceptionPage();

            UseDevelopmentStaticFiles(app, root, "node_modules");
            UseDevelopmentStaticFiles(app, root, "bower_components");
            UseDevelopmentStaticFiles(app, root, "scripts");
            UseDevelopmentStaticFiles(app, root, "styles");

        }

        private static void UseDevelopmentStaticFiles(IApplicationBuilder app, string root, string relativePath)
        {
            var provider = new FileExtensionContentTypeProvider();
            if (relativePath == "styles") provider.Mappings.Add(".scss", "text/x.scss");

            var absolutePath = Path.Combine(root, relativePath);

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/" + relativePath,
                FileProvider = new PhysicalFileProvider(absolutePath),
                ContentTypeProvider = provider
            });
        }

    }
}
