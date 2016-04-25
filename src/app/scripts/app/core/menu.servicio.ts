import { Injectable } from "angular2/core"
import { Http, Headers, RequestMethod } from "angular2/http"
import { IOpcionDeMenu } from "./modelos";

@Injectable()
export class MenuServicio {

    private url = "http://localhost:5002/api/core/menu/principal/opciones";

    constructor(private http: Http) { }

    obtenerOpciones() {
        console.log("asfd");
        console.log([this.http, this.url]);
        return this.http.get(this.url).map(x => x.json() as IOpcionDeMenu[]);
    }

}