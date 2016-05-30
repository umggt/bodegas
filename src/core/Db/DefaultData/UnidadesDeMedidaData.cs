using Bodegas.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.DefaultData
{
    internal static class UnidadesDeMedidaData
    {
        internal static BodegasContext GenerarUnidadesDeMedida(this BodegasContext db)
        {
            if (db.UnidadesDeMedida.Any())
            {
                return db;
            }

            db.UnidadesDeMedida.AddRange(new[] {
                new UnidadDeMedida { Nombre = "Unidades" },
                new UnidadDeMedida { Nombre = "Cajas" },
                new UnidadDeMedida { Nombre = "Litros" },
                new UnidadDeMedida { Nombre = "Resmas" }
            });

            db.SaveChanges();
            return db;
        }
    }
}
