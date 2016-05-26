using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Db.Entities
{
    public class Proveedor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Nombre { get; set; }

        [StringLength(1000)]
        public string Direccion { get; set; }

        [StringLength(200)]
        public string NombreDeContacto { get; set; }

        public ICollection<ProveedorTelefono> Telefonos { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }

        public int UsuarioCreacionId { get; set; }
        public int UsuarioModificaId { get; set; }

        public Usuario UsuarioCreacion { get; set; }
        public Usuario UsuarioModifica { get; set; }

        /// <summary>
        /// Productos que pueden ingresar de este proveedor
        /// </summary>
        public ICollection<ProveedorProducto> Productos { get; set; }

        public Proveedor()
        {
            FechaCreacion = DateTime.UtcNow;
            FechaModificacion = FechaCreacion;
        }
    }
}
