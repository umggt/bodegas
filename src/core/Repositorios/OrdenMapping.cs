using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bodegas.Repositorios
{
    /// <summary>
    /// Clase auxiliar que facilita la lectura de codigo cuando se implementa paginación.
    /// <example>
    ///     Se utiliza para poder escribir en lugar de esto:
    ///     <code>
    ///     <![CDATA[
    ///         var orderMapping = new Dictionary<string, Expression<Func<UsuarioResumen, object>>> {
    ///             { "id", x => x.Id },
    ///             { "login", x => x.Login },
    ///             { "nombre", x => x.Nombre },
    ///             { "correo", x => x.Correo },
    ///             { "activo", x=> x.Activo }
    ///         };
    ///     ]]>
    ///     </code>
    /// 
    ///     Poder escribir esto:
    ///     <![CDATA[
    ///         var orderMapping = new OrdenMapping<UsuarioResumen> {
    ///             { "id", x => x.Id },
    ///             { "login", x => x.Login },
    ///             { "nombre", x => x.Nombre },
    ///             { "correo", x => x.Correo },
    ///             { "activo", x=> x.Activo }
    ///         };
    ///     ]]>
    ///     </code>
    /// </example>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class OrdenMapping<T> : Dictionary<string, Expression<Func<T, object>>>
    {
    }
}
