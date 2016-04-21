using Microsoft.AspNet.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
