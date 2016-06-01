using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Bodegas.Modelos;
using Microsoft.Data.Entity;

namespace Bodegas.Repositorios
{
    internal static class RepositoriosExtensiones
    {
        
        internal static async Task<PaginacionResultado<T>> OrdenarAsync<T>(this IQueryable<T> query, PaginacionParametros paginacion, Expression<Func<T, object>> defaultOrder = null, OrdenMapping<T> orderMapping = null, bool orderByDefaultDesc = false)
        {
            IOrderedQueryable<T> orderedQuery = null;

            if (orderMapping != null && paginacion.Ordenamiento != null && paginacion.Ordenamiento.Count > 0)
            {
                foreach (var columna in paginacion.Ordenamiento)
                {
                    if (orderMapping.ContainsKey(columna.Key))
                    {
                        var fn = orderMapping[columna.Key];

                        if (columna.Value)
                        {
                            orderedQuery = orderedQuery == null ? query.OrderBy(fn) : orderedQuery.ThenBy(fn);
                        }
                        else
                        {
                            orderedQuery = orderedQuery == null ? query.OrderByDescending(fn) : orderedQuery.ThenByDescending(fn);
                        }
                    }
                }
            }

            if (orderedQuery == null)
            {
                orderedQuery = orderByDefaultDesc ? query.OrderByDescending(defaultOrder) : query.OrderBy(defaultOrder);
            }

            if (paginacion.ElementosPorPagina < 1 || paginacion.ElementosPorPagina > 100)
            {
                paginacion.ElementosPorPagina = 20;
            }

            var totalElementos = query.Count();
            var totalPaginas = (int)Math.Ceiling(totalElementos / (double)paginacion.ElementosPorPagina);

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

            var result = new PaginacionResultado<T>
            {
                Elementos = elementos,
                Pagina = paginacion.Pagina,
                ElementosPorPagina = paginacion.ElementosPorPagina,
                CantidadElementos = elementos.Length,
                TotalElementos = totalElementos,
                TotalPaginas = totalPaginas,
                PaginaSiguiente = paginacion.Pagina == totalPaginas ? null as int? : paginacion.Pagina + 1,
                PaginaAnterior = paginacion.Pagina == 1 ? null as int? : paginacion.Pagina - 1,
                Paginas = Enumerable.Range(1, totalPaginas).ToArray()
            };
            return result;
        }

    }
}
