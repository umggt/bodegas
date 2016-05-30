import { Injectable } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { Headers, RequestMethod, URLSearchParams } from "@angular/http"
import { UnidadDeMedida } from "./unidades-de-medida.modelos"
import { PaginacionResultado, PaginacionParametros } from "../modelos"
import { HttpServicio } from "../http.servicio"

@Injectable()
export class UnidadesDeMedidaServicio {

    private url = "http://localhost:5002/api/core/unidadesdemedida/";

    constructor(private http: HttpServicio) { }

    public obtenerTodos(paginacion?: PaginacionParametros): Observable<PaginacionResultado<UnidadDeMedida>> {
        var params = this.http.params(paginacion);
        return this.http.get(this.url, { search: params }).map(x => x.json() as PaginacionResultado<UnidadDeMedida>);
    }

    obtenerUnica(id: number): Observable<UnidadDeMedida> {
        return this.http.get(this.url + id).map(x => x.json() as UnidadDeMedida);
    }

    guardar(unidadMedida: UnidadDeMedida): Observable<UnidadDeMedida> {

        const body = JSON.stringify(unidadMedida);
        let url = this.url;
        let method = RequestMethod.Post;

        // Si la entidad tiene un Id, se hace un HTTP PUT a /api/core/entidades/id
        // en lugar de un HTTP POST a /api/core/entidades/
        if (unidadMedida.id) {
            url = this.url + unidadMedida.id;
            method = RequestMethod.Put;
        }

        return this.http.request(url, { body: body, method: method }).map(x => x.json() as UnidadDeMedida);
    }


}