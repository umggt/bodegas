import { Injectable, } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { RolResumen } from "./modelos"
import { PaginacionResultado } from "../modelos"
import { HttpServicio } from "../http.servicio"

@Injectable()
export class RolesServicio {

    private url = "http://localhost:5002/api/core/roles/";

    constructor(private http: HttpServicio) { }

    obtenerTodos(): Observable<PaginacionResultado<RolResumen>> {
        return this.http.get(this.url).map(x => x.json() as PaginacionResultado<RolResumen>);
    }

}