using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Bodegas.Auth;
using Bodegas.Db;
using Microsoft.AspNet.FileProviders;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.AspNet.StaticFiles;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace Bodegas.App
{
    public class Startup
    {
        private readonly IApplicationEnvironment applicationEnvironment;
        private readonly IConfigurationRoot configuration;

        public Startup(IApplicationEnvironment applicationEnvironment, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            this.applicationEnvironment = applicationEnvironment;

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            configuration = builder.Build();

            loggerFactory.AddSerilog();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var certificatePath = Path.Combine(applicationEnvironment.ApplicationBasePath, "certificados", "idsrv4test.pfx");
            var certificate = new X509Certificate2(certificatePath, "idsrv3test");

            services.AddAuthenticationServer(certificate);
            services.AddMvc(ConfigureJsonFormatter).AddAuthenticationViewLocationExpander();

            var defaultConnectionString = configuration["Data:DefaultConnection:ConnectionString"];

            services.AddEntityFramework().AddSqlite().AddDbContext<BodegasContext>(options => options.UseSqlite(defaultConnectionString));

        }

        private static void ConfigureJsonFormatter(MvcOptions options)
        {
            var jsonFormatter = options.OutputFormatters.First(x => x is JsonOutputFormatter) as JsonOutputFormatter;
            Debug.Assert(jsonFormatter != null, "jsonFormatter != null");
            var settings = jsonFormatter.SerializerSettings;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseIISPlatformHandler();

            app.UseHtml5Routes("/index.html");

            var defaultFiles = new DefaultFilesOptions { DefaultFileNames = new[] { "index.html" } };
            app.UseDefaultFiles(defaultFiles);
            app.UseStaticFiles();


            if (env.IsDevelopment())
            {
                var root = applicationEnvironment.ApplicationBasePath;
                ConfigureDevelopmentEnvironment(app, root);

                CreateDatabaseDirectory(env);
                app.MigrateDatabase();
            }

            app.Map(new PathString("/auth"), config =>
            {
                config.UseIdentityServer();
                config.UseMvcWithDefaultRoute();
            });

            app.Map(new PathString("/api"), config =>
            {
                config.UseMvc();
            });

        }

        private static void CreateDatabaseDirectory(IHostingEnvironment env)
        {
            var databaseDirectory = Path.Combine(env.WebRootPath, "data");
            if (!Directory.Exists(databaseDirectory))
            {
                Directory.CreateDirectory(databaseDirectory);
            }
        }


        private void ConfigureDevelopmentEnvironment(IApplicationBuilder app, string root)
        {
            app.UseDeveloperExceptionPage();

            UseDevelopmentStaticFiles(app, root, "node_modules");
            UseDevelopmentStaticFiles(app, root, "bower_components");
            UseDevelopmentStaticFiles(app, root, "scripts");
            UseDevelopmentStaticFiles(app, root, "styles");

            app.Map(new PathString("/app"), config => { config.Use(TemplatesFromScriptsPath); });
        }

        private async Task TemplatesFromScriptsPath(HttpContext context, Func<Task> next)
        {

            var fileRelativePath = context.Request.Path.Value.TrimStart('/');
            if (fileRelativePath.EndsWith(".html"))
            {
                var root = applicationEnvironment.ApplicationBasePath;
                var fileAbsolutePath = Path.Combine(root, "scripts", "app", fileRelativePath);
                if (File.Exists(fileAbsolutePath))
                {
                    var fileInfo = new FileInfo(fileAbsolutePath);

                    context.Response.StatusCode = 200;
                    context.Response.ContentType = "text/html";
                    context.Response.ContentLength = fileInfo.Length;

                    using (var fs = fileInfo.OpenRead())
                    {
                        var length = (int)fileInfo.Length;
                        await fs.CopyToAsync(context.Response.Body, length, context.RequestAborted);
                    }

                    return;
                }
            }

            await next();

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

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
