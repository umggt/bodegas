﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Modelos
{
    public class ProductoEgresoDetalle
    {
        public int ProductoId { get; set; }
        public int EgresoId { get; set; }
        public int UnidadId { get; set; }
        public string UnidadDeMedida { get; set; }
        public decimal Cantidad { get; set; }
        public int MarcaId { get; set; }
    }
}
