using System.ComponentModel.DataAnnotations;

namespace Bodegas.Db.Entities
{
    public class ListaValor
    {
        public int Id { get; set; }

        public int ListaId { get; set; }

        public Lista Lista { get; set; }

        [Required]
        [StringLength(100)]
        public string Valor { get; set; }
    }
}