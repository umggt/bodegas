import { Injectable } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { Headers, RequestMethod, URLSearchParams } from "@angular/http"
import { ListaResumen, Lista, ListaValor } from "./listas.modelos"
import { PaginacionResultado, PaginacionParametros } from "../modelos"
import { HttpServicio } from "../http.servicio"

@Injectable()
export class ListasServicio {

    private url = "http://localhost:5002/api/core/listas/";

    constructor(private http: HttpServicio) { }

    obtenerTodas(paginacion?: PaginacionParametros): Observable<PaginacionResultado<ListaResumen>> {
        var params = this.http.params(paginacion);
        return this.http.get(this.url, { search: params }).map(x => x.json() as PaginacionResultado<ListaResumen>);
    }

    obtenerUnica(id: number): Observable<Lista> {
        return this.http.get(this.url + id).map(x => x.json() as Lista);
    }

    guardar(lista: Lista): Observable<Lista> {
        const body = JSON.stringify(lista);
        let url = this.url;
        let method = RequestMethod.Post;

        // Si la entidad tiene un Id, se hace un HTTP PUT a /api/core/entidades/id
        // en lugar de un HTTP POST a /api/core/entidades/
        if (lista.id) {
            url = this.url + lista.id;
            method = RequestMethod.Put;
        }

        return this.http.request(url, { body: body, method: method }).map(x => x.json() as Lista);
    }

    guardarValor(valor: string, idLista: number): Observable<ListaValor> {   
        const body = JSON.stringify({valor: valor});    
        let url = this.url + idLista + "/valores";
        let method = RequestMethod.Post;
        return this.http.request(url, { body: body, method: method }).map(x => x.json() as ListaValor);
    }

    eliminarValor(idLista: number, idValor: number) {
        const body = JSON.stringify({ idLista: idLista, idValor: idValor });
        let url = this.url + idLista + "/valores";
        let method = RequestMethod.Delete;
        return this.http.request(url, { body: body, method: method }).map(x => x.json() as ListaValor);
    }
}