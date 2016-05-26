using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Bodegas.Repositorios
{
    internal class OrdenMapping<T> : Dictionary<string, Expression<Func<T, object>>>
    {
    }
}
