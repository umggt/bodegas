using Bodegas.Db;
using Bodegas.Repositorios;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System.IO;

namespace Bodegas
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services, string connectionString, string applicationBasePath)
        {
            connectionString = connectionString.Replace("|ApplicationBasePath|", Path.GetFullPath(applicationBasePath));
            services.AddEntityFramework().AddSqlite().AddDbContext<BodegasContext>(options => options.UseSqlite(connectionString));
            services.AddTransient<UsuariosRepositorio>();
            services.AddTransient<RolesRepositorio>();
            services.AddTransient<ProductosRepositorio>();
            services.AddTransient<ListasRepositorio>();
            services.AddTransient<BodegasRepositorio>();
            services.AddTransient<ProveedoresRepositorio>();
        }

    }
}
