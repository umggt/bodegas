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
            var nombresDeMarcas2 = new[] {"Lenovo", "Toshiba"};

            var marcas = db.Marcas.Where(x => nombresDeMarcas.Contains(x.Nombre)).ToArray();
            var marcas2 = db.Marcas.Where(x => nombresDeMarcas2.Contains(x.Nombre)).ToArray();

            if (!marcas.Any() || !marcas2.Any())
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

            db.Productos.AddRange(new[]
            {
                new Producto
                {
                    Nombre = "Computadora portatil",
                    Descripcion = "Computadora personal portable",
                    Marcas = marcas2.Select(x => new ProductoMarca
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
                },
                new Producto
                {
                    Nombre = "Computadora de escritorio",
                    Descripcion = "Estación de trabajo",
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
                            Nombre = "Tiene Monitor",
                            TipoCaracteristica = ProductoTipoCaracteristica.Booleano
                        },
                        new ProductoCaracteristica
                        {
                            Nombre = "Tiene Teclado",
                            TipoCaracteristica = ProductoTipoCaracteristica.Booleano
                        },
                        new ProductoCaracteristica
                        {
                            Nombre = "Color",
                            TipoCaracteristica = ProductoTipoCaracteristica.TextoCorto
                        }
                    }
                }
            });

            db.SaveChanges();

            return db;
        }
    }
}
