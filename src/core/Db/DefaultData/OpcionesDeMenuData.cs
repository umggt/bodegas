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
        internal static BodegasContext GenerarOpcionesDeMenu(this BodegasContext context)
        {
            if (context.OpcionesDeMenu.Any())
            {
                return context;
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

            context.OpcionesDeMenu.Add(opcionDashboard);
            context.OpcionesDeMenu.Add(opcionManteminientos);
            context.OpcionesDeMenu.Add(opcionSeguridad);
            context.SaveChanges();
            return context;
        }
    }
}