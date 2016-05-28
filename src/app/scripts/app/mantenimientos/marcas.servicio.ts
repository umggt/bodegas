import { Injectable } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { Headers, RequestMethod, URLSearchParams } from "@angular/http"
import { Marca } from "./marcas.modelos"
import { PaginacionResultado, PaginacionParametros } from "../modelos"
import { HttpServicio } from "../http.servicio"

@Injectable()
export class MarcasServicio {

    private url = "http://localhost:5002/api/core/marcas/";

    constructor(private http: HttpServicio) { }

    public obtenerTodos(paginacion?: PaginacionParametros): Observable<PaginacionResultado<Marca>> {
        var params = this.http.params(paginacion);
        return this.http.get(this.url, { search: params }).map(x => x.json() as PaginacionResultado<Marca>);
    }

}