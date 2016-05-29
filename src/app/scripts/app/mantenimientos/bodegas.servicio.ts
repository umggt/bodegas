import { Injectable } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { Headers, RequestMethod, URLSearchParams } from "@angular/http"
import { BodegaResumen } from "./bodegas.modelos"
import { PaginacionResultado, PaginacionParametros } from "../modelos"
import { HttpServicio } from "../http.servicio"

@Injectable()
export class BodegasServicio {

    private url = "http://localhost:5002/api/core/bodegas/";

    constructor(private http: HttpServicio) { }

    obtenerTodas(paginacion?: PaginacionParametros): Observable<PaginacionResultado<BodegaResumen>> {
        var params = this.http.params(paginacion);
        return this.http.get(this.url, {search: params}).map(x => x.json() as PaginacionResultado<BodegaResumen>);
    }

    obtenerUnica(id: number): Observable<BodegaResumen> {
        return this.http.get(this.url + id).map(x => x.json() as BodegaResumen);
    }

    guardar(bodega: BodegaResumen): Observable<BodegaResumen> {
      
        const body = JSON.stringify(bodega);
        let url = this.url;
        let method = RequestMethod.Post;

        // Si la entidad tiene un Id, se hace un HTTP PUT a /api/core/entidades/id
        // en lugar de un HTTP POST a /api/core/entidades/
        if (bodega.id) {
            url = this.url + bodega.id;
            method = RequestMethod.Put;
        }

        return this.http.request(url, { body: body, method: method }).map(x => x.json() as BodegaResumen);
    }
}