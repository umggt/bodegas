using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class IngresoProductoCaracteristicaDetalle
    {
        public int Id { get; set; }
        public int IngresoProductoId { get; set; }
        public string Nombre { get; set; }
        public int? ListaValorId { get; set; }
        public string Valor { get; set; }
        public bool EsNumero { get; set; }
        public bool EsLista { get; set; }
        public bool EsMoneda { get; set; }
        public bool EsTexto { get; set; }
        public bool EsBooleano { get; set; }
        public string ListaValor { get; internal set; }
    }
}
