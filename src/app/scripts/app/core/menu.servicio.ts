import { Injectable } from "angular2/core"
import { Http, Headers, RequestMethod } from "angular2/http"
import { IOpcionDeMenu } from "./modelos";

@Injectable()
export class MenuServicio {

    private url = "http://localhost:5002/api/core/menu/principal/opciones";

    constructor(private http: Http) { }

    obtenerOpciones() {

        const headers = new Headers();
        headers.append("Authorization", `Bearer ${Bodega.tokenManager.access_token}`);

        return this.http.get(this.url, { headers: headers }).map(x => x.json() as IOpcionDeMenu[]);
    }

}