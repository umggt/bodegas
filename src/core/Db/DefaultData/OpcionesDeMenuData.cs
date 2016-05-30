using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db.Entities;
using Serilog;

namespace Bodegas.Db.DefaultData
{
    internal static class OpcionesDeMenuData
    {
        internal static BodegasContext GenerarOpcionesDeMenu(this BodegasContext db)
        {
            if (db.OpcionesDeMenu.Any())
            {
                return db;
            }

            Log.Debug("Generando las opciones de menú por defecto.");

            var opcionDashboard = new OpcionDeMenu
            {
                Titulo = "Dashboard",
                Icono = "fa-dashboard",
                Ruta = "Dashboard"
            };

            var opcionGestiones = new OpcionDeMenu
            {
                Titulo = "Gestiones",
                Icono = "fa-cogs",
                Opciones = new []
                {
                    new OpcionDeMenu
                    {
                        Titulo = "Ingresos",
                        Ruta = "IngresosListado"
                    },
                    new OpcionDeMenu
                    {
                        Titulo = "Egresos",
                        Ruta = "EgresosListado"
                    },
                    new OpcionDeMenu
                    {
                        Titulo = "Traslados",
                        Ruta = "TrasladosListado"
                    }
                }
            };

            var opcionManteminientos = new OpcionDeMenu
            {
                Titulo = "Mantenimientos",
                Icono = "fa-database",
                Opciones = new[]
                {
                    new OpcionDeMenu
                    {
                        Titulo = "Bodegas",
                        Ruta = "BodegasListado"
                    },
                    new OpcionDeMenu
                    {
                        Titulo = "Productos",
                        Ruta = "ProductosListado"
                    },
                    new OpcionDeMenu
                    {
                        Titulo = "Listas",
                        Ruta = "ListasListado"
                    },
                    new OpcionDeMenu
                    {
                        Titulo = "Proveedores",
                        Ruta = "ProveedoresListado"
                    },
                    new OpcionDeMenu
                    {
                        Titulo = "Marcas",
                        Ruta = "MarcasListado"
                    },
                    new OpcionDeMenu
                    {
                        Titulo = "Unidades De Medida",
                        Ruta = "UnidadesDeMedidaListado"
                    }
                }
            };

            var opcionSeguridad = new OpcionDeMenu
            {
                Titulo = "Seguridad",
                Icono = "fa-lock",
                Opciones = new[]
                {
                    new OpcionDeMenu
                    {
                        Titulo = "Usuarios",
                        Ruta = "UsuariosListado"
                    }
                }
            };

            db.OpcionesDeMenu.Add(opcionDashboard);
            db.OpcionesDeMenu.Add(opcionGestiones);
            db.OpcionesDeMenu.Add(opcionManteminientos);
            db.OpcionesDeMenu.Add(opcionSeguridad);
            db.SaveChanges();
            return db;
        }
    }
}
