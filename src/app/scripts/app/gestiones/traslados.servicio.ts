import { Injectable } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { HttpServicio } from "../http.servicio"
import { Traslado } from "./traslados.modelos"

@Injectable()
export class TrasladosServicio {

    private url = "http://localhost:5002/api/core/traslados/";

    constructor(private http: HttpServicio) {
    }

    public guardar(ingreso: Traslado): Observable<Traslado> {
        return this.http.post(this.url, ingreso).map(x => x.json() as Traslado);
    }
}