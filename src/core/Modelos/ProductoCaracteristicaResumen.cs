using Bodegas.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class ProductoCaracteristicaResumen
    {
        public int Id { get; set; }
        public ProductoTipoCaracteristica Tipo { get; set; }
        public string Nombre { get; set; }
        public int? ListaId { get; set; }
        public int? ListaValorId { get; set; }
        public string Valor { get; set; }
        public decimal? Minimo { get; set; }
        public decimal? Maximo { get; set; }
        public bool Requerido { get; set; }
        public bool EsLista { get; set; }
        public bool EsNumero { get; set; }
        public bool EsMoneda { get; set; }
        public bool EsTextoCorto { get; set; }
        public bool EsTextoLargo { get; set; }
        public bool EsBooleano { get; set; }
        public ListaValorDetalle[] Valores { get; set; }
    }
}
