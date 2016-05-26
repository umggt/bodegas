using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class Bodega
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [StringLength(1000)]
        public string Direccion { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        public int UsuarioCreacionId { get; set; }
        public int UsuarioModificaId { get; set; }

        public Usuario UsuarioCreacion { get; set; }
        public Usuario UsuarioModifica { get; set; }

        public Bodega()
        {
            FechaCreacion = DateTime.UtcNow;
            FechaModificacion = FechaCreacion;
        }

    }
}
