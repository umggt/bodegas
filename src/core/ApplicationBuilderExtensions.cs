using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;

namespace Bodegas
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseHtml5Routes(this IApplicationBuilder app, string spaStartupPoint)
        {
            return app.Use(async (context, next) =>
            {
                await next();

                // If there's no available file and the request doesn't contain an extension, we're probably trying to access a page.
                // Rewrite request to use app root
                if (context.Response.StatusCode == 404 && !Path.HasExtension(context.Request.Path.Value))
                {
                    context.Request.Path = spaStartupPoint;
                    await next();
                }
            });
        }

        public static IApplicationBuilder MigrateDatabase(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                serviceScope.ServiceProvider.GetService<BodegasContext>().Database.Migrate();
                serviceScope.ServiceProvider.GetService<BodegasContext>().EnsureSeedData();
            }

            return app;
        }

        public static IApplicationBuilder CreateDatabaseDirectory(this IApplicationBuilder app, string applicationBasePath)
        {
            var databaseDirectory = Path.Combine(applicationBasePath, "data");
            if (!Directory.Exists(databaseDirectory))
            {
                Directory.CreateDirectory(databaseDirectory);
            }

            return app;
        }
    }
}
