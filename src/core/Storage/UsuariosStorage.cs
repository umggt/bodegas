using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db;
using Bodegas.Modelos;
using Microsoft.Data.Entity;

namespace Bodegas.Storage
{
    public class UsuariosStorage
    {
        private readonly BodegasContext db;

        public UsuariosStorage(BodegasContext db)
        {
            this.db = db;
        }

        public async Task<PaginacionResultado<UsuarioResumen>> ObtenerTodosAsync(PaginacionParametros paginacion)
        {
            var query = from n in db.Usuarios
                select new UsuarioResumen
                {
                    Id = n.Id,
                    Login = n.Login,
                    Nombre = n.NombreCompleto,
                    Correo = n.Correo,
                    Activo = n.Activo
                };

            IOrderedQueryable<UsuarioResumen> orderedQuery = null;

            if (paginacion.Ordenamiento != null && paginacion.Ordenamiento.Count > 0)
            {
                foreach (var columna in paginacion.Ordenamiento)
                {
                    if (columna.Key == "id")
                    {
                        if (columna.Value)
                        {
                            orderedQuery = orderedQuery == null ? query.OrderBy(x => x.Id) : orderedQuery.ThenBy(x => x.Id);
                        }
                        else
                        {
                            orderedQuery = orderedQuery == null ? query.OrderByDescending(x => x.Id) : orderedQuery.ThenByDescending(x => x.Id);
                        }
                        
                    }

                    if (columna.Key == "login")
                    {
                        if (columna.Value)
                        {
                            orderedQuery = orderedQuery == null ? query.OrderBy(x => x.Login) : orderedQuery.ThenBy(x => x.Login);
                        }
                        else
                        {
                            orderedQuery = orderedQuery == null ? query.OrderByDescending(x => x.Login) : orderedQuery.ThenByDescending(x => x.Login);
                        }
                    }

                    if (columna.Key == "etiqueta")
                    {
                        if (columna.Value)
                        {
                            orderedQuery = orderedQuery == null ? query.OrderBy(x => x.Nombre) : orderedQuery.ThenBy(x => x.Nombre);
                        }
                        else
                        {
                            orderedQuery = orderedQuery == null ? query.OrderByDescending(x => x.Nombre) : orderedQuery.ThenByDescending(x => x.Nombre);
                        }
                    }

                    if (columna.Key == "correo")
                    {
                        if (columna.Value)
                        {
                            orderedQuery = orderedQuery == null ? query.OrderBy(x => x.Correo) : orderedQuery.ThenBy(x => x.Correo);
                        }
                        else
                        {
                            orderedQuery = orderedQuery == null ? query.OrderByDescending(x => x.Correo) : orderedQuery.ThenByDescending(x => x.Correo);
                        }
                    }
                }
            }

            if (orderedQuery == null)
            {
                orderedQuery = query.OrderBy(x => x.Id);
            }

            if (paginacion.ElementosPorPagina < 1 || paginacion.ElementosPorPagina > 100)
            {
                paginacion.ElementosPorPagina = 20;
            }


            var totalElementos = query.Count();
            var totalPaginas = (int) Math.Ceiling(totalElementos / (double) paginacion.ElementosPorPagina);

            if (paginacion.Pagina < 1)
            {
                paginacion.Pagina = 1;
            }

            if (paginacion.Pagina > totalPaginas)
            {
                paginacion.Pagina = totalPaginas;
            }

            var skip = (paginacion.Pagina - 1) * paginacion.ElementosPorPagina;
            var take = paginacion.ElementosPorPagina;
            var resultado = orderedQuery.Skip(skip).Take(take);

            var elementos = await resultado.ToArrayAsync();

            var result = new PaginacionResultado<UsuarioResumen>
            {
                Elementos = elementos,
                Pagina = paginacion.Pagina,
                ElementosPorPagina = paginacion.ElementosPorPagina,
                CantidadElementos = elementos.Length,
                TotalElementos = totalElementos,
                TotalPaginas = totalPaginas,
                PaginaSiguiente = paginacion.Pagina == totalPaginas ? null as int? : paginacion.Pagina + 1,
                PaginaAnterior = paginacion.Pagina == 1 ? null as int? : paginacion.Pagina - 1,
                Paginas = Enumerable.Range(1, totalPaginas - 1).ToArray()
            };

            return result;
        }

        public async Task<UsuarioDetalle> ObtenerUnicoAsync(int id)
        {
            var usuario = await db.Usuarios.Include(x => x.Atributos).Include(x => x.Roles).ThenInclude(x => x.Rol).SingleAsync(x => x.Id == id);

            return new UsuarioDetalle
            {
                Id = usuario.Id,
                Login = usuario.Login,
                NombreCompleto = usuario.NombreCompleto,
                Nombres = usuario.Nombres,
                Apellidos = usuario.Apellidos,
                Correo = usuario.Correo,
                CorreoVerificado = usuario.CorreoVerificado,
                SitioWeb = usuario.SitioWeb,
                Activo = usuario.Activo,
                Atributos = usuario.Atributos.GroupBy(x => x.Nombre).ToDictionary(x => x.Key, x => x.Select(y => y.Valor).ToArray()),
                Roles = usuario.Roles.ToDictionary(x => x.RolId, x => x.Rol.Nombre)
            };
        }
    }
}
