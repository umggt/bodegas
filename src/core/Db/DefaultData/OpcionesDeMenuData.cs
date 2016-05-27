﻿using System;
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
            db.OpcionesDeMenu.Add(opcionManteminientos);
            db.OpcionesDeMenu.Add(opcionSeguridad);
            db.SaveChanges();
            return db;
        }
    }
}
