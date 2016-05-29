import { Injectable } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { Headers, RequestMethod, URLSearchParams } from "@angular/http"
import { ProveedorResumen, ProveedorDetalle } from "./proveedores.modelos"
import { PaginacionResultado, PaginacionParametros } from "../modelos"
import { HttpServicio } from "../http.servicio"


@Injectable()
export class ProveedoresServicio {

    private url = "http://localhost:5002/api/core/proveedores/";

    constructor(private http: HttpServicio) { }

    obtenerTodos(paginacion?: PaginacionParametros): Observable<PaginacionResultado<ProveedorResumen>> {
        var params = this.http.params(paginacion);
        return this.http.get(this.url, { search: params }).map(x => x.json() as PaginacionResultado<ProveedorResumen>);
    }

    obtenerUnico(id: number): Observable<ProveedorDetalle> {
        return this.http.get(this.url + id).map(x => x.json() as ProveedorDetalle);
    }

    guardar(proveedor: ProveedorDetalle): Observable<ProveedorDetalle> {

        const body = JSON.stringify(proveedor);
        let url = this.url;
        let method = RequestMethod.Post;

        // Si la entidad tiene un Id, se hace un HTTP PUT a /api/core/entidades/id
        // en lugar de un HTTP POST a /api/core/entidades/
        if (proveedor.id) {
            url = this.url + proveedor.id;
            method = RequestMethod.Put;
        }

        return this.http.request(url, { body: body, method: method }).map(x => x.json() as ProveedorDetalle);
    }
}