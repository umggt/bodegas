using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Hosting.Internal;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Bodegas.App
{
    public class Program
    {
        // Entry point for the application.
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.ColoredConsole()
                .CreateLogger();

            var auth = StartAuthServer(args);
            var coreApi = StartCoreApiServer(args);
            var ui = StartUIServer(args);
            
            Console.WriteLine("Servicios Iniciados");
            Console.ReadLine();

            ui.Dispose();
            coreApi.Dispose();
            auth.Dispose();
        }

        private static string[] Parametros(string[] args, params string[] masArgs)
        {
            return args.Union(masArgs).ToArray();
        }


        private static IApplication StartUIServer(string[] args)
        {
            return StartServer<Startup>(args, "http://*:5000");
        }

        private static IApplication StartAuthServer(string[] args)
        {
            return StartServer<Auth.Startup>(args, "http://*:5001");
        }

        private static IApplication StartCoreApiServer(string[] args)
        {
            return StartServer<Bodegas.Startup>(args, "http://*:5002");
        }

        private static IApplication StartServer<T>(string[] args, string url) where T : class
        {
            var configArgs = Parametros(args, "--server.urls", url);

            var config = new ConfigurationBuilder()
                .AddCommandLine(configArgs)
                .AddEnvironmentVariables();
            var s = config.GetBasePath();
            Console.WriteLine(s);
            var host = new WebHostBuilder(config.Build())
               .UseServer("Microsoft.AspNet.Server.Kestrel")
               .UseStartup<T>()
               .Build();

            return host.Start();
        }
    }
}
