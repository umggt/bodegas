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
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc.Filters;

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
            services.AddMvc(ConfigureMvc);
            //services.AddCors(options => options.AddPolicy("AllowAll", p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));


            var defaultConnectionString = configuration["Data:DefaultConnection:ConnectionString"];
            if (string.IsNullOrWhiteSpace(defaultConnectionString)) return;

            defaultConnectionString = defaultConnectionString.Replace("|ApplicationBasePath|", Path.GetFullPath(applicationEnvironment.ApplicationBasePath));
            services.AddEntityFramework().AddSqlite().AddDbContext<BodegasContext>(options => options.UseSqlite(defaultConnectionString));
        }

        public static void ConfigureMvc(MvcOptions options)
        {
            var jsonFormatter = options.OutputFormatters.First(x => x is JsonOutputFormatter) as JsonOutputFormatter;
            Debug.Assert(jsonFormatter != null, "jsonFormatter != null");
            var settings = jsonFormatter.SerializerSettings;
            settings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var policy = new AuthorizationPolicyBuilder()
            //This is what makes it function like the basic [Authorize] attribute
            .RequireAuthenticatedUser()
            //add functionality similar to [Authorize(Roles="myrole")]
            //.RequireRole("myrole")
            //add functionality similar to [ClaimsAuthorize("myclaim")]
            //.RequireClaim("myclaim")
            .Build();

            options.Filters.Add(new AuthorizeFilter(policy));

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseIISPlatformHandler();

            if (env.IsDevelopment())
            {
                var root = applicationEnvironment.ApplicationBasePath;
                CreateDatabaseDirectory(root);
                app.MigrateDatabase();
            }

            app.Map(new PathString("/api/core"), config =>
            {
                config.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
                config.UseIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5001/auth";
                    options.ScopeName = "bodegas.api";
                    //options.ScopeSecret = "secret";

                    options.AutomaticAuthenticate = true;
                    options.AutomaticChallenge = true;
                });
                config.UseMvc(routes => {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller}/{id?}"
                    );
                });
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
