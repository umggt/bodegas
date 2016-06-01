import { Injectable } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { Resumen } from "./dashboard.modelos";
import { HttpServicio } from "../http.servicio"


@Injectable()
export class DashboardServicio {

    private url = "http://localhost:5002/api/core/dashboard/";

    constructor(private http: HttpServicio) {
        
    }

    public obtenerResumen(): Observable<Resumen> {
        return this.http.get(this.url + 'resumen').map(x => x.json() as Resumen);
    }
}