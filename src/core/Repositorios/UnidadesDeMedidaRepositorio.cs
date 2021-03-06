﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Modelos;
using Bodegas.Db;
using Microsoft.Data.Entity;
using Bodegas.Db.Entities;
using Bodegas.Exceptions;

namespace Bodegas.Repositorios
{
    public class UnidadesDeMedidaRepositorio
    {
        private readonly BodegasContext db;

        public UnidadesDeMedidaRepositorio(BodegasContext db)
        {
            this.db = db;
        }

        public async Task<PaginacionResultado<UnidadDeMedidaResumen>> ObtenerTodasAsync(PaginacionParametros paginacion)
        {
            var query = from q in db.UnidadesDeMedida
                        select new UnidadDeMedidaResumen
                        {
                            Id = q.Id,
                            Nombre = q.Nombre
                        };

            return await query.OrdenarAsync(paginacion, x => x.Id);
        }

        internal async Task<PaginacionResultado<UnidadDeMedidaResumen>> ObtenerPorProductoAsync(int productoId, PaginacionParametros paginacion)
        {
            var query = from p in db.ProductoUnidadesDeMedida
                        where p.ProductoId == productoId
                        select new UnidadDeMedidaResumen
                        {
                            Id = p.UnidadDeMedida.Id,
                            Nombre = p.UnidadDeMedida.Nombre
                        };

            return await query.Distinct().OrdenarAsync(paginacion, x => x.Nombre);
        }

        public async Task<UnidadDeMedidaResumen> ObtenerUnicaAsync(int id)
        {
            var unidadMedida = await db.UnidadesDeMedida.SingleAsync(x => x.Id == id);

            return new UnidadDeMedidaResumen
            {
                Id = unidadMedida.Id,
                Nombre = unidadMedida.Nombre
            };
        }

        public async Task<int> CrearAsync(UnidadDeMedidaResumen unidadMedida)
        {
            var nombre = unidadMedida.Nombre.ToLowerInvariant();

            if (await ExisteUnidadDeMedida(nombre))
            {
                throw new InvalidOperationException($"La unidad de medida '{nombre}' ya existe.");
            }

            var nuevaUnidad = new UnidadDeMedida
            {
                Nombre = nombre

            };

            db.UnidadesDeMedida.Add(nuevaUnidad);
            var filasAfectadas = await db.SaveChangesAsync();
            if (filasAfectadas > 0)
            {
                return nuevaUnidad.Id;
            }
            else
            {
                return -1;
            }
        }

        private Task<bool> ExisteUnidadDeMedida(string nombre)
        {
            return db.UnidadesDeMedida.AnyAsync(x => x.Nombre.ToLower() == nombre);
        }

        public async Task<bool> EditarAsync(int id, UnidadDeMedidaResumen unidadMedida)
        {
            var unidadMedidaAEditar = await db.UnidadesDeMedida.SingleOrDefaultAsync(x => x.Id == id);

            if (unidadMedidaAEditar == null)
            {
                throw new RegistroNoEncontradoException($"No existe la unidad de medida {id} ");

            }

            unidadMedidaAEditar.Nombre = unidadMedida.Nombre.Trim();

            var filasAfectas = await db.SaveChangesAsync();
            return filasAfectas > 0;

        }

        public async Task<bool> EliminarUnidadDeMedidaAsync(int id)
        {
            if (await ExisteUnidadDeMedidaAsignada(id))
            {
                throw new RegistroNoEncontradoException($"La unidad de medida {id} ya está asignada a un producto.");
            }
            var unidadDeMedidaAEliminar = await db.UnidadesDeMedida.SingleOrDefaultAsync(x => x.Id == id);

            if (unidadDeMedidaAEliminar == null)
            {
                throw new RegistroNoEncontradoException($"No existe la unidad de medida {id}");
            }

            db.UnidadesDeMedida.Remove(unidadDeMedidaAEliminar);

            var filasAfectadas = await db.SaveChangesAsync();
            return filasAfectadas > 0;
        }

        private Task<bool> ExisteUnidadDeMedidaAsignada(int id)
        {
            return db.ProductoUnidadesDeMedida.AnyAsync(x => x.UnidadDeMedidaId == id);
        }

    }
}
