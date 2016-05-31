using Bodegas.Db;
using Bodegas.Db.Entities;
using Bodegas.Modelos;
using Microsoft.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bodegas.Repositorios
{
    public class CaracteristicasRepositorio
    {
        private readonly BodegasContext db;

        public CaracteristicasRepositorio(BodegasContext db)
        {
            this.db = db;
        }

        public async Task<ProductoCaracteristicaResumen[]> ObtenerPorProducto(int productoId)
        {
            var query = from pc in db.ProductoCaracteristicas
                        where pc.ProductoId == productoId
                        select new ProductoCaracteristicaResumen
                        {
                            Id = pc.Id,
                            Nombre = pc.Nombre,
                            Tipo = pc.TipoCaracteristica,
                            Requerido = pc.Requerido,
                            Minimo = pc.Minimo,
                            Maximo = pc.Maximo,
                            EsBooleano = pc.TipoCaracteristica == ProductoTipoCaracteristica.Booleano,
                            EsLista = pc.TipoCaracteristica == ProductoTipoCaracteristica.SeleccionSimple || pc.TipoCaracteristica == ProductoTipoCaracteristica.SeleccionMultiple,
                            EsTextoCorto = pc.TipoCaracteristica == ProductoTipoCaracteristica.TextoCorto,
                            EsTextoLargo = pc.TipoCaracteristica == ProductoTipoCaracteristica.TextoLargo,
                            EsMoneda = pc.TipoCaracteristica == ProductoTipoCaracteristica.Moneda,
                            EsNumero = pc.TipoCaracteristica >= ProductoTipoCaracteristica.NumeroEntero && pc.TipoCaracteristica <= ProductoTipoCaracteristica.NumeroDecimalPositivo,
                            ListaId = pc.ListaId
                            //Valores = pc.Lista.Valores.OrderBy(x => x.Valor).Select(x => new ListaValorDetalle {
                            //    Id = x.Id,
                            //    Valor = x.Valor
                            //})
                        };

            var result = await query.ToArrayAsync();
            var resultConLista = result.Where(x => x.ListaId.HasValue).ToArray();
            var listas = resultConLista.Select(x => x.ListaId).ToArray();
            var valores = db.ListaValores.Where(x => listas.Contains(x.ListaId)).ToArray();

            foreach (var item in resultConLista)
            {
                item.Valores = valores.Where(x => x.ListaId == item.ListaId).Select(x => new ListaValorDetalle {
                    Id = x.Id,
                    Valor = x.Valor
                }).ToArray();
            }

            return result;
        }
    }
}
