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

    obtenerUnica(id: number): Observable<Marca> {
        return this.http.get(this.url + id).map(x => x.json() as Marca);
    }

    guardar(marca: Marca): Observable<Marca> {

        const body = JSON.stringify(marca);
        let url = this.url;
        let method = RequestMethod.Post;

        // Si la entidad tiene un Id, se hace un HTTP PUT a /api/core/entidades/id
        // en lugar de un HTTP POST a /api/core/entidades/
        if (marca.id) {
            url = this.url + marca.id;
            method = RequestMethod.Put;
        }

        return this.http.request(url, { body: body, method: method }).map(x => x.json() as Marca);
    }

    eliminarMarca(id: number) {
        let url = this.url + id;

        return this.http.delete(url);
    }
}