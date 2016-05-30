using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;

namespace Bodegas.Db.DefaultData
{
    internal static class ProductosData
    {
        internal static BodegasContext GenerarProductos(this BodegasContext db)
        {
            if (db.Productos.Any())
            {
                return db;
            }

            var nombresDeMarcas = new[] { "HP", "Dell" };
            var marcas = db.Marcas.Where(x => nombresDeMarcas.Contains(x.Nombre)).ToArray();

            if (!marcas.Any())
            {
                return db;
            }

            var unidades = db.UnidadesDeMedida.FirstOrDefault(x => x.Nombre == "Unidades");

            if (unidades == null)
            {
                return db;
            }

            var sistemasOperativos = db.Listas.FirstOrDefault(x => x.Nombre == "Sistemas Operativos");
            if (sistemasOperativos == null)
            {
                return db;
            }

            db.Productos.Add(new Producto
            {
                Nombre = "Computadora portatil",
                Descripcion = "Computadora personal portable",
                Marcas = marcas.Select(x => new ProductoMarca
                {
                    Marca = x
                }).ToList(),
                UnidadesDeMedida = new[] 
                {
                    new ProductoUnidadDeMedida
                    {
                        UnidadDeMedida = unidades
                    }
                },
                Caracteristicas = new[]
                {
                    new ProductoCaracteristica
                    {
                        Nombre = "Sistema Operativo",
                        TipoCaracteristica = ProductoTipoCaracteristica.SeleccionSimple,
                        Lista = sistemasOperativos
                    },
                    new ProductoCaracteristica
                    {
                        Nombre = "Memoria (en GB)",
                        TipoCaracteristica = ProductoTipoCaracteristica.NumeroDecimalPositivo,
                        Minimo = 0,
                        Requerido = true
                    },
                    new ProductoCaracteristica
                    {
                        Nombre = "Disco Duro (en GB)",
                        TipoCaracteristica = ProductoTipoCaracteristica.NumeroDecimalPositivo,
                        Minimo = 0,
                        Requerido = true
                    },
                    new ProductoCaracteristica
                    {
                        Nombre = "Procesador (en GHz)",
                        TipoCaracteristica = ProductoTipoCaracteristica.NumeroDecimalPositivo,
                        Minimo = 0,
                        Requerido = true
                    }
                }
            });

            db.SaveChanges();

            return db;
        }
    }
}
