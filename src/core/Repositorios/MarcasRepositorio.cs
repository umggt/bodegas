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
    public class MarcasRepositorio
    {
        private readonly BodegasContext db;

        public MarcasRepositorio(BodegasContext db)
        {
            this.db = db;
        }

        public async Task<PaginacionResultado<MarcaResumen>> ObtenerTodasAsync(PaginacionParametros paginacion)
        {
            var query = from q in db.Marcas
                        select new MarcaResumen
                        {
                            Id = q.Id,
                            Nombre = q.Nombre
                        };

            return await query.OrdenarAsync(paginacion, x => x.Id);
        }

        public async Task<MarcaResumen> ObtenerUnicaAsync(int id)
        {
            var marca = await db.Marcas.SingleAsync(x => x.Id == id);

            return new MarcaResumen
            {
                Id = marca.Id,
                Nombre = marca.Nombre
            };
        }

        public async Task<int> CrearAsync(MarcaResumen marca)
        {
            var nombre = marca.Nombre.ToLowerInvariant();

            if (await ExisteMarca(nombre))
            {
                throw new InvalidOperationException($"La marca '{nombre}' ya existe.");
            }

            var nuevaMarca = new Marca
            {
                Nombre = nombre
               
            };

            db.Marcas.Add(nuevaMarca);
            var filasAfectadas = await db.SaveChangesAsync();
            if (filasAfectadas > 0)
            {
                return nuevaMarca.Id;
            }
            else
            {
                return -1;
            }
        }

        private Task<bool> ExisteMarca(string nombre)
        {
            return db.Marcas.AnyAsync(x => x.Nombre.ToLower() == nombre);
        }

        public async Task<bool> EditarAsync(int id, MarcaResumen marca)
        {
            var marcaAEditar = await db.Marcas.SingleOrDefaultAsync(x => x.Id == id);

            if (marcaAEditar == null)
            {
                throw new RegistroNoEncontradoException($"No existe la marca {id} ");

            }

            marcaAEditar.Nombre = marca.Nombre.Trim();

            var filasAfectas = await db.SaveChangesAsync();
            return filasAfectas > 0;

        }
    }
}
