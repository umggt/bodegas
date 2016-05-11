using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class RolPermiso
    {
        public int RolId { get; set; }
        public int PermisoId { get; set; }

        public Rol Rol { get; set; }
        public Permiso Permiso { get; set; }
    }
}
