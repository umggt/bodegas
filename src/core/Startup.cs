using Bodegas.Db;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Formatters;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Serilog;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Bodegas
{
    public class Startup
    {
        private readonly IApplicationEnvironment applicationEnvironment;
        private readonly IConfigurationRoot configuration;

        public Startup(IApplicationEnvironment applicationEnvironment, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            this.applicationEnvironment = applicationEnvironment;

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            configuration = builder.Build();

            loggerFactory.AddSerilog();
        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(ConfigureJsonFormatter);

            var defaultConnectionString = configuration["Data:DefaultConnection:ConnectionString"].Replace("|ApplicationBasePath|", Path.GetFullPath(applicationEnvironment.ApplicationBasePath));

            services.AddEntityFramework().AddSqlite().AddDbContext<BodegasContext>(options => options.UseSqlite(defaultConnectionString));

        }

        public static void ConfigureJsonFormatter(MvcOptions options)
        {
            var jsonFormatter = options.OutputFormatters.First(x => x is JsonOutputFormatter) as JsonOutputFormatter;
            Debug.Assert(jsonFormatter != null, "jsonFormatter != null");
            var settings = jsonFormatter.SerializerSettings;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseIISPlatformHandler();
            app.UseCors(x => x.AllowAnyOrigin());

            if (env.IsDevelopment())
            {
                var root = applicationEnvironment.ApplicationBasePath;
                CreateDatabaseDirectory(root);
                app.MigrateDatabase();
            }

            app.Map(new PathString("/api/core"), config =>
            {
                config.UseMvc();
            });

        }

        private static void CreateDatabaseDirectory(string root)
        {
            var databaseDirectory = Path.Combine(root, "data");
            if (!Directory.Exists(databaseDirectory))
            {
                Directory.CreateDirectory(databaseDirectory);
            }
        }
    }
}
