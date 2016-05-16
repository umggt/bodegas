using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bodegas.Db;
using Bodegas.Modelos;
using Microsoft.Data.Entity;
using Bodegas.Db.Entities;
using Bodegas.Exceptions;

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

        public async Task<int> CrearAsync(UsuarioDetalle usuario)
        {
            var login = usuario.Login.Trim().ToLowerInvariant();
            var correo = usuario.Correo.Trim().ToLowerInvariant();

            if (await ExisteLoginInterno(login))
            {
                throw new InvalidOperationException($"El login '{login}' ya está asignado a otro usuario.");
            }

            if (await ExisteCorreoInterno(correo))
            {
                throw new InvalidOperationException($"El correo '{correo}' ya está asignado a otro usuario.");
            }

            var nuevoUsuario = new Usuario {
                Login = login,
                Nombres = usuario.Nombres.Trim(),
                Apellidos= usuario.Apellidos.Trim(),
                NombreCompleto = usuario.NombreCompleto.Trim(),
                Correo = correo,
                SitioWeb = usuario.SitioWeb?.Trim().ToLowerInvariant(),
                Activo = false,
                CorreoVerificado = false,
                Clave = new byte[] { }
            };

            if (usuario.Roles != null && usuario.Roles.Count > 0)
            {
                var rolesIds = usuario.Roles.Keys;
                var roles = db.Roles.Where(x => rolesIds.Contains(x.Id)).Distinct().ToList();
                if (rolesIds.Count != roles.Count)
                {
                    throw new InvalidOperationException("Al menos uno de los roles especificados no es válido.");
                }

                nuevoUsuario.Roles = roles.Select(x => new UsuarioRol
                {
                    Usuario = nuevoUsuario,
                    Rol = x
                }).ToList();
            }

            if (usuario.Atributos != null && usuario.Atributos.Count > 0)
            {
                nuevoUsuario.Atributos = new List<UsuarioAtributo>();

                foreach (var atributo in usuario.Atributos)
                {
                    if (string.IsNullOrWhiteSpace(atributo.Key)) continue;

                    foreach (var valor in atributo.Value)
                    {
                        if (string.IsNullOrEmpty(valor)) continue;

                        nuevoUsuario.Atributos.Add(new UsuarioAtributo
                        {
                            Nombre = atributo.Key.Trim().ToLowerInvariant(),
                            Valor = valor.Trim(),
                            Usuario = nuevoUsuario
                        });
                    }
                    
                }
            }

            db.Usuarios.Add(nuevoUsuario);
            var filasAfectadas = await db.SaveChangesAsync();
            if (filasAfectadas > 0)
            {
                return nuevoUsuario.Id;
            }
            else
            {
                return -1;
            }
        }

        public async Task<bool> EditarAsync(int id, UsuarioDetalle usuario)
        {
            var usuarioAEditar = await db.Usuarios.Include(x => x.Atributos).Include(x => x.Roles).ThenInclude(x => x.Rol).SingleOrDefaultAsync(x => x.Id == id);

            if (usuarioAEditar == null)
            {
                throw new RegistroNoEncontradoException($"No existe el usuario {id}");
            }

            var correo = usuario.Correo.Trim().ToLowerInvariant();
            if (await ExisteCorreoEnOtroUsuario(id, correo))
            {
                throw new InvalidOperationException($"El correo {correo} ya está asginado a otro usuario.");
            }

            usuarioAEditar.Nombres = usuario.Nombres.Trim();
            usuarioAEditar.Apellidos = usuario.Apellidos.Trim();
            usuarioAEditar.NombreCompleto = usuario.NombreCompleto.Trim();
            usuarioAEditar.Correo = correo;
            usuarioAEditar.SitioWeb = usuario.SitioWeb?.Trim().ToLowerInvariant();

            var atributos = usuario.Atributos ?? new Dictionary<string, string[]>();
            var roles = usuario.Roles ?? new Dictionary<int, string>();

            var rolesAEliminar = usuarioAEditar.Roles.Where(x => !roles.Keys.Contains(x.RolId)).ToArray();
            var rolesAInsertar = roles.Where(x => !usuarioAEditar.Roles.Select(y => y.RolId).Contains(x.Key)).ToArray();

            var atributosAEliminar = usuarioAEditar.Atributos.Where(x => !atributos.Keys.Contains(x.Nombre, StringComparer.OrdinalIgnoreCase)).ToArray();
            var atributosAEditar = usuarioAEditar.Atributos.Where(x => atributos.Keys.Contains(x.Nombre, StringComparer.OrdinalIgnoreCase)).GroupBy(x => x.Nombre).ToDictionary(x => x.Key, x => x.ToArray());
            var atributosAInsertar = atributos.Where(x => !usuarioAEditar.Atributos.Select(y => y.Nombre).Contains(x.Key, StringComparer.OrdinalIgnoreCase)).ToArray();

            foreach (var rol in rolesAEliminar)
            {
                usuarioAEditar.Roles.Remove(rol);
            }

            foreach (var rol in rolesAInsertar)
            {
                usuarioAEditar.Roles.Add(new UsuarioRol {
                    RolId = rol.Key,
                    Usuario = usuarioAEditar
                });
            }
            foreach (var atributo in atributosAEliminar)
            {
                usuarioAEditar.Atributos.Remove(atributo);
            }

            foreach (var atributo in atributosAInsertar)
            {
                if (string.IsNullOrWhiteSpace(atributo.Key)) continue;

                foreach (var valor in atributo.Value)
                {
                    if (string.IsNullOrEmpty(valor)) continue;

                    usuarioAEditar.Atributos.Add(new UsuarioAtributo
                    {
                        Nombre = atributo.Key.Trim().ToLowerInvariant(),
                        Valor = valor.Trim(),
                        Usuario = usuarioAEditar
                    });
                }
            }

            // En lugar de este algoritmo sería mas fácil eliminar todos los
            // atributos que tiene el usuario y agregar como nuevos los que
            // se envían en el modelo, pero a fin de ejercicio y de reutilizar
            // los ids con los que ya cuentan los atributos se realiza este
            // algoritmo.
            foreach (var atributo in atributosAEditar)
            {
                var esteAtributo = atributo.Value;
                var otroAtributo = atributos[atributo.Key];
                var longitudOriginal = esteAtributo.Length;
                int i;

                if (longitudOriginal == otroAtributo.Length)
                {
                    for (i = 0; i < longitudOriginal; i++)
                    {
                        var atributoOriginal = esteAtributo[i];
                        var nuevoValor = otroAtributo[i];
                        if (atributoOriginal.Valor != nuevoValor)
                        {
                            atributoOriginal.Valor = nuevoValor;
                        }
                    }
                }
                else if (longitudOriginal < otroAtributo.Length)
                {
                    for (i = 0; i < longitudOriginal; i++)
                    {
                        var atributoOriginal = esteAtributo[i];
                        var nuevoValor = otroAtributo[i];
                        if (atributoOriginal.Valor != nuevoValor)
                        {
                            atributoOriginal.Valor = nuevoValor;
                        }
                    }

                    for (; i < otroAtributo.Length; i++)
                    {
                        usuarioAEditar.Atributos.Add(new UsuarioAtributo
                        {
                            Nombre = atributo.Key,
                            Valor = otroAtributo[i].Trim(),
                            Usuario = usuarioAEditar
                        });
                    }
                }
                else if (longitudOriginal > otroAtributo.Length)
                {
                    for (i = 0; i < otroAtributo.Length; i++)
                    {
                        var atributoOriginal = esteAtributo[i];
                        var nuevoValor = otroAtributo[i];
                        if (atributoOriginal.Valor != nuevoValor)
                        {
                            atributoOriginal.Valor = nuevoValor;
                        }
                    }

                    for (; i < longitudOriginal; i++)
                    {
                        usuarioAEditar.Atributos.Remove(esteAtributo[i]);
                    }
                }
            }
                 
            var filasAfectadas = await db.SaveChangesAsync();
            return filasAfectadas > 0;
        }

        public Task<bool> ActivarAsync(int id) {
            return ModificarValorActivo(id, true);
        }

        public Task<bool> DesactivarAsync(int id)
        {
            return ModificarValorActivo(id, false);
        }

        public Task<bool> ExisteLogin(string login)
        {
            login = login.Trim().ToLowerInvariant();
            return ExisteLoginInterno(login);
        }

        public Task<bool> ExisteCorreo(string correo)
        {
            correo = correo.Trim().ToLowerInvariant();
            return ExisteCorreoInterno(correo);
        }

        private Task<bool> ExisteLoginInterno(string login)
        {
            return db.Usuarios.AnyAsync(x => x.Login.ToLowerInvariant() == login);
        }

        private Task<bool> ExisteCorreoInterno(string correo)
        {
            return db.Usuarios.AnyAsync(x => x.Correo.ToLowerInvariant() == correo);
        }

        private Task<bool> ExisteCorreoEnOtroUsuario(int usuarioIdIgnorar, string correo)
        {
            return db.Usuarios.AnyAsync(x => x.Id != usuarioIdIgnorar && x.Correo.ToLowerInvariant() == correo);
        }


        private async Task<bool> ModificarValorActivo(int id, bool valor)
        {
            var usuario = await db.Usuarios.SingleOrDefaultAsync(x => x.Id == id);

            if (usuario == null)
            {
                throw new RegistroNoEncontradoException($"No existe el usuario {id}");
            }

            if (usuario.Activo != valor)
            {
                usuario.Activo = valor;
                var filasAfectadas = await db.SaveChangesAsync();
                return filasAfectadas > 0;
            }

            return true;
        }
    }
}
