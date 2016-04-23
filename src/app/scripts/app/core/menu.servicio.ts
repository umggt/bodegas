import { Injectable } from "angular2/core"
import { Http, Headers, RequestMethod } from "angular2/http"
import { IOpcionDeMenu } from "./modelos";

@Injectable()
export class MenuServicio {

    private url = "/api/core/opcionesdemenu/";

    constructor(private http: Http) { }

    obtenerOpciones() {
        console.log("asfd");
        console.log([this.http, this.url]);
        return this.http.get(this.url).map(x => x.json() as IOpcionDeMenu[]);
    }

}