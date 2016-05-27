import { Injectable } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { Headers, RequestMethod, URLSearchParams } from "@angular/http"
import { ListaResumen, Lista } from "./modelos"
import { PaginacionResultado, PaginacionParametros } from "../modelos"
import { HttpServicio } from "../http.servicio"

@Injectable()
export class ListasServicio {

    private url = "http://localhost:5002/api/core/listas/";

    constructor(private http: HttpServicio) { }

    obtenerTodas(paginacion?: PaginacionParametros): Observable<PaginacionResultado<ListaResumen>> {
        var params = this.http.params(paginacion);
        return this.http.get(this.url, { search: params }).map(x => x.json() as PaginacionResultado<ListaResumen>);
    }
}