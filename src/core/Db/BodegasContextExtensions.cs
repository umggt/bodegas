using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace Bodegas.Db
{
    public static class BodegasContextExtensions
    {
        public static void EnsureSeedData(this BodegasContext context)
        {
            if (context.AllMigrationsApplied())
            {
                BodegasDefaultData.Generar(context);
            }
            else
            {
                Log.Debug("No se han aplicado todas las migrations de BodegasContext (no se genera data).");
            }
        }
    }
}
