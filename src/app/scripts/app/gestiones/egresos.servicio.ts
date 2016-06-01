import { Injectable } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { Headers, RequestMethod, URLSearchParams } from "@angular/http"
import { EgresoResumen, EgresoDetalle } from "./egresos.modelos"
import { PaginacionResultado, PaginacionParametros } from "../modelos"
import { HttpServicio } from "../http.servicio"
import { UnidadesDeMedidaServicio } from "../mantenimientos/unidades-de-medida.servicio"
import { MarcasServicio } from "../mantenimientos/marcas.servicio"


@Injectable()
export class EgresosServicio {

    private url = "http://localhost:5002/api/core/egresos/";
    

    constructor(private http: HttpServicio, private unidadesDeMedida: UnidadesDeMedidaServicio, private marcasServicio: MarcasServicio) {
    }

    public obtenerTodos(paginacion?: PaginacionParametros): Observable<PaginacionResultado<EgresoResumen>> {
        var params = this.http.params(paginacion);
        return this.http.get(this.url, { search: params }).map(x => x.json() as PaginacionResultado<EgresoResumen>);
    }

    public obtenerUnico(id: number): Observable<EgresoDetalle> {
        return this.http.get(this.url + id).map(x => x.json() as EgresoDetalle);
    }

    public guardar(egreso: EgresoDetalle): Observable<EgresoDetalle> {
        const body = JSON.stringify(egreso);
        let url = this.url;
        let method = RequestMethod.Post;

        // Si la entidad tiene un Id, se hace un HTTP PUT a /api/core/entidades/id
        // en lugar de un HTTP POST a /api/core/entidades/
        if (egreso.id) {
            url = this.url + egreso.id;
            method = RequestMethod.Put;
        }

        return this.http.request(url, { body: body, method: method }).map(x => x.json() as EgresoDetalle);
    }



    public obtenerMarcas(productoId: number) {
        return this.marcasServicio.obtenerPorProducto(productoId);
    }

}