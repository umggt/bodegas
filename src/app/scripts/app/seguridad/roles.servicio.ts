import { Injectable, } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { Http, Headers, RequestMethod } from "@angular/http"
import { RolResumen } from "./modelos"
import { PaginacionResultado } from "../core/modelos"

@Injectable()
export class RolesServicio {

    private url = "http://localhost:5002/api/core/roles/";

    constructor(private http: Http) { }

    obtenerTodos(): Observable<PaginacionResultado<RolResumen>> {
        const headers = new Headers();
        headers.append("Authorization", `Bearer ${Bodega.tokenManager.access_token}`);

        return this.http.get(this.url, { headers: headers }).map(x => x.json() as PaginacionResultado<RolResumen>);
    }

}