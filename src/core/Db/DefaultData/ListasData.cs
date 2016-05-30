using Bodegas.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.DefaultData
{
    internal static class ListasData
    {
        internal static BodegasContext GenerarListas(this BodegasContext db)
        {
            if (db.Listas.Any())
            {
                return db;
            }

            db.Listas.AddRange(new[] {
                new Lista {
                    Nombre = "Paises",
                    Valores = new [] {
                        new ListaValor { Valor = "Guatemala" },
                        new ListaValor { Valor = "Honduras" },
                        new ListaValor { Valor = "Nicaragua" },
                        new ListaValor { Valor = "El Salvador" },
                        new ListaValor { Valor = "Belice" },
                        new ListaValor { Valor = "Costa Rica" },
                        new ListaValor { Valor = "Panamá" }
                    }
                },
                new Lista
                {
                    Nombre = "Estado",
                    Valores = new []
                    {
                        new ListaValor { Valor = "Nuevo" },
                        new ListaValor { Valor = "Usado" },
                        new ListaValor { Valor = "Inservible" }
                    }
                },
                new Lista
                {
                    Nombre = "Sistemas Operativos",
                    Valores = new []
                    {
                        new ListaValor { Valor = "Windows" },
                        new ListaValor { Valor = "Linux" },
                        new ListaValor { Valor = "OS X" }
                    }
                }
            });

            db.SaveChanges();
            return db;
        }
    }
}
