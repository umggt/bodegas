using System;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db;
using Bodegas.Modelos;
using Microsoft.Data.Entity;
using Bodegas.Db.Entities;
using Bodegas.Exceptions;

namespace Bodegas.Repositorios
{
    public class ListasRepositorio
    {
        private readonly BodegasContext db;

        public ListasRepositorio(BodegasContext db)
        {
            this.db = db;
        }

        public async Task<PaginacionResultado<ListaResumen>> ObtenerTodasAsync(PaginacionParametros paginacion)
        {
            var query = from n in db.Listas
                        select new ListaResumen
                        {
                            Id = n.Id,                           
                            Nombre = n.Nombre                           
                        };

            // Aqui se mapean los paremetros que envía la UI a sus respectivas propiedad
            // para poder realizar el ordenamiento.
            // Por ejemplo, si la UI envía ordenamiento=login,-nombre este mapping
            // identifica que "login" corresponde a la propiedad "Login" de UsuarioResumen
            // y que "nombre" corresponde a la propiedad "Nombre" de UsuarioResumen.
            var orderMapping = new OrdenMapping<ListaResumen> {
                { "id", x => x.Id },              
                { "nombre", x => x.Nombre }            
            };

            var result = await query.OrdenarAsync(paginacion, x => x.Id, orderMapping);

            return result;
        }

        public async Task<ListaDetalle> ObtenerUnicaAsync(int id)
        {
            var lista = await db.Listas.Include(x => x.Valores).SingleAsync(x => x.Id == id);

            return new ListaDetalle
            {
                Id = lista.Id,
                Nombre = lista.Nombre,
                Valores = lista.Valores.Select(x=>new ListaValorDetalle { Id = x.Id, Valor= x.Valor}).ToList() 
            };
        }

        public async Task<int> CrearAsync(ListaDetalle lista)
        {
            var nombre = lista.Nombre.Trim();            

            if (await ExisteNombre(nombre))
            {
                throw new InvalidOperationException($"Ya existe una lista con el nombre '{nombre}'.");
            }

            var nuevaLista = new Lista
            {
                Nombre = nombre
            };

            db.Listas.Add(nuevaLista);

            var filasAfectadas = await db.SaveChangesAsync();
            if (filasAfectadas > 0)
            {
                return nuevaLista.Id;
            }
            else
            {
                return -1;
            }
        }  

        public async Task<bool> EditarAsync(int id, ListaDetalle lista)
        {
            var listaAEditar = await db.Listas.Include(x=> x.Valores).SingleOrDefaultAsync(x => x.Id == id);

            if (listaAEditar == null)
            {
                throw new RegistroNoEncontradoException($"No existe la lista {id}");
            }

            var nombre = lista.Nombre.Trim();
            if (await ExisteNombreEnOtraLista(id, nombre))
            {
                throw new InvalidOperationException($"Ya existe otra lista con el nombre '{nombre}'.");
            }

            listaAEditar.Nombre = nombre;            

            var filasAfectadas = await db.SaveChangesAsync();
            return filasAfectadas > 0;
        }

        private Task<bool> ExisteNombre(string nombre)
        {
            return db.Listas.AnyAsync(x => x.Nombre.ToLower() == nombre.ToLower());
        }

        private async Task<bool> ExisteNombreValor(int idLista, string nombre)
        {
            return await db.ListaValores.AnyAsync(x => x.ListaId == idLista && x.Valor.ToLower() == nombre.ToLower());
        }

        private Task<bool> ExisteNombreEnOtraLista(int listaAIgnorar, string nombre)
        {
            return db.Listas.AnyAsync(x => x.Id != listaAIgnorar && x.Nombre.ToLower() == nombre.ToLower());
        }

        public async Task<ListaValor> CrearValorAsync(int idLista, string valor)
        {
            if (await ExisteNombreValor(idLista, valor.Trim()))
            {
                throw new InvalidOperationException($"Ya existe una valor en la lista con el nombre '{valor}'.");
            }

            var nuevoValor = new ListaValor
            {                 
                ListaId = idLista,
                Valor = valor
            };

            db.ListaValores.Add(nuevoValor);

            var filasAfectadas = await db.SaveChangesAsync();
            if (filasAfectadas > 0)
            {
                return nuevoValor;
            }

            return null;
            
        }
    }
}
