using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Serilog;

namespace Bodegas.Db
{
    public static class BodegasDefaultData
    {
        public static void Generar(BodegasContext context)
        {
            if (!context.OpcionesDeMenu.Any())
            {
                GenerarOpcionesDeMenu(context);
            }
        }

        private static void GenerarOpcionesDeMenu(BodegasContext context)
        {
            Log.Debug("Generando las opciones de menú por defecto.");

            var opcionDashboard = new OpcionDeMenu
            {
                Titulo = "Dashboard",
                Ruta = "Dashboard"
            };

            var opcionManteminientos = new OpcionDeMenu
            {
                Titulo = "Mantenimientos",
                Opciones = new[]
                {
                    new OpcionDeMenu
                    {
                        Titulo = "Bodegas",
                        Ruta = "BodegasListado"
                    }
                }
            };

            context.OpcionesDeMenu.Add(opcionDashboard);
            context.OpcionesDeMenu.Add(opcionManteminientos);
            context.SaveChanges();
        }
    }
}
