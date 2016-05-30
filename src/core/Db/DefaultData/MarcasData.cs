using Bodegas.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.DefaultData
{
    internal static class MascarData
    {
        internal static BodegasContext GenerarMarcas(this BodegasContext db)
        {
            if (db.Marcas.Any())
            {
                return db;
            }

            db.Marcas.AddRange(new[] {
                new Marca { Nombre = "Dell" },
                new Marca { Nombre = "HP" },
                new Marca { Nombre = "Lenovo" },
                new Marca { Nombre = "Toshiba" }
            });

            db.SaveChanges();
            return db;
        }
    }
}
