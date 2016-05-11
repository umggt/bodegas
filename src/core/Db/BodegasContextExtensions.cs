using Bodegas.Db.DefaultData;
using Serilog;

namespace Bodegas.Db
{
    public static class BodegasContextExtensions
    {
        public static void EnsureSeedData(this BodegasContext context)
        {
            if (context.AllMigrationsApplied())
            {
                context.GenerarDatos();
            }
            else
            {
                Log.Debug("No se han aplicado todas las migrations de BodegasContext (no se genera data).");
            }
        }

        private static void GenerarDatos(this BodegasContext context)
        {
            context
                .GenerarOpcionesDeMenu()
                .GenerarUsuarios();
        }
    }
}
