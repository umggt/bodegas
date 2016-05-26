using System.ComponentModel.DataAnnotations;

namespace Bodegas.Db.Entities
{
    public class ListaValor
    {
        public int ListaId { get; set; }

        [Required]
        [StringLength(100)]
        public string Valor { get; set; }
    }
}