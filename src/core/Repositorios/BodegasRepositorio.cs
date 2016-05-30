using Bodegas.Db;
using Bodegas.Db.Entities;
using Bodegas.Exceptions;
using Bodegas.Modelos;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Repositorios
{
    public class BodegasRepositorio
    {
        private readonly BodegasContext db;

        public BodegasRepositorio(BodegasContext db)
        {
            this.db = db;
        }

        public async Task<PaginacionResultado<BodegaResumen>> ObtenerTodasAsync(PaginacionParametros paginacion)
        {
            var query = from n in db.Bodegas
                        select new BodegaResumen
                        {
                            Id = n.Id,
                            Nombre = n.Nombre,
                            Direccion = n.Direccion
                        };

            var orderMapping = new OrdenMapping<BodegaResumen> {
                { "id", x => x.Id },
                { "nombre", x => x.Nombre },
                { "direccion", x => x.Direccion }
            };

            var result = await query.OrdenarAsync(paginacion, x => x.Id, orderMapping);

            return result;
        }

        public async Task<BodegaResumen> ObtenerUnicaAsync(int id)
        {
            var bodega = await db.Bodegas.SingleAsync(x => x.Id == id);

            return new BodegaResumen
            {
                Id = bodega.Id,
                Nombre = bodega.Nombre,
                Direccion = bodega.Direccion
            };
        }

        public async Task<int> CrearAsync(BodegaResumen bodega, int usuarioId)
        {
            var nombre = bodega.Nombre.ToLowerInvariant();
            var direccion = bodega.Direccion.ToLowerInvariant();

            if (await ExisteBodega(nombre))
            {
                throw new InvalidOperationException($"La bodega '{nombre}' ya existe.");
            }

            var nuevaBodega = new Bodega
            {
                Nombre = nombre,
                Direccion = direccion,
                UsuarioCreacionId = usuarioId,
                UsuarioModificaId = usuarioId
            };

            db.Bodegas.Add(nuevaBodega);
            var filasAfectadas = await db.SaveChangesAsync();
            if (filasAfectadas > 0)
            {
                return nuevaBodega.Id;
            }
            else
            {
                return -1;
            }
        }

        private Task<bool> ExisteBodega(string nombre)
        {
            return db.Bodegas.AnyAsync(x => x.Nombre.ToLower() == nombre);
        }

        public async Task<bool> EditarAsync(int id, BodegaResumen bodega, int usuarioId)
        {
            var bodegaAEditar = await db.Bodegas.SingleOrDefaultAsync(x => x.Id == id);

            if (bodegaAEditar == null)
            {
                throw new RegistroNoEncontradoException($"No existe la bodega {id} ");

            }

            bodegaAEditar.Nombre = bodega.Nombre.Trim();
            bodegaAEditar.Direccion = bodega.Direccion.Trim();
            bodegaAEditar.UsuarioModificaId = usuarioId;

            var filasAfectas = await db.SaveChangesAsync();
            return filasAfectas > 0;
            
        }
    }
}
