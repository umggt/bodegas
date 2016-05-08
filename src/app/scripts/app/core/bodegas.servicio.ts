import { Injectable } from "@angular/core"
import { Http, Headers, RequestMethod } from "@angular/http"
import { IBodega, IBodegaResumen } from "./modelos"
import "rxjs/add/operator/map";

@Injectable()
export class BodegasServicio {

    private url = "/api/core/bodegas/";

    constructor(private http: Http) { }

    obtenerTodas() {
        return this.http.get(this.url).map(x => x.json() as IBodegaResumen[]);
    }

    obtenerUnica(id: number) {
        return this.http.get(this.url + id).map(x => x.json() as IBodega);
    }

    guardar(entidad: IBodega) {
        const headers = new Headers();
        const body = JSON.stringify(entidad);
        let url = this.url;
        let method = RequestMethod.Post;

        // Si la entidad tiene un Id, se hace un HTTP PUT a /api/core/entidades/id
        // en lugar de un HTTP POST a /api/core/entidades/
        if (entidad.id) {
            url = this.url + entidad.id;
            method = RequestMethod.Put;
        }

        headers.append("Content-Type", "application/json");
        headers.append("Authorization", `Bearer ${Bodega.tokenManager.access_token}`);
        return this.http.request(url, { body: body, headers: headers, method: method }).map(x => x.json() as IBodega);
    }
}