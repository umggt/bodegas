import { Injectable } from "angular2/core"
import { Http, Headers, RequestMethod } from "angular2/http"
import { Router } from "angular2/router"
import { IOpcionDeMenu } from "./modelos";

@Injectable()
export class MenuServicio {

    private url = "http://localhost:5002/api/core/menu/principal/opciones";

    constructor(private http: Http, private router: Router) { }

    obtenerOpciones() {

        const headers = new Headers();
        headers.append("Authorization", `Bearer ${Bodega.tokenManager.access_token}`);

        return this.http.get(this.url, { headers: headers }).map(resultado => {
            var opciones = resultado.json() as IOpcionDeMenu[];

            opciones.forEach(opcion => {

                if (!opcion.tieneOpciones) {
                    return;
                }

                var expandido = false;

                opcion.opciones.forEach(opcionHija => {
                    if (opcionHija.ruta) {
                        var instruction = this.router.generate([opcionHija.ruta]);
                        expandido = this.router.isRouteActive(instruction);
                    }
                });

                opcion.expandido = expandido;

            });


            return opciones;
        });
    }
}