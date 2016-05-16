import { Injectable, } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { Http, Headers, RequestMethod } from "@angular/http"
import { UsuarioResumen, Usuario } from "./modelos"
import { PaginacionResultado } from "../modelos"

@Injectable()
export class UsuariosServicio {

    private url = "http://localhost:5002/api/core/usuarios/";

    constructor(private http: Http) { }

    obtenerTodos(): Observable<PaginacionResultado<UsuarioResumen>> {
        const headers = new Headers();
        headers.append("Authorization", `Bearer ${Bodega.tokenManager.access_token}`);

        return this.http.get(this.url, { headers: headers }).map(x => x.json() as PaginacionResultado<UsuarioResumen>);
    }

    obtenerUnico(id: number): Observable<Usuario> {
        const headers = new Headers();
        headers.append("Authorization", `Bearer ${Bodega.tokenManager.access_token}`);
        return this.http.get(this.url + id, { headers: headers }).map(x => x.json() as Usuario);
    }

    guardar(usuario: Usuario): Observable<Usuario> {
        const headers = new Headers();
        const body = JSON.stringify(usuario);
        let url = this.url;
        let method = RequestMethod.Post;

        // Si la entidad tiene un Id, se hace un HTTP PUT a /api/core/entidades/id
        // en lugar de un HTTP POST a /api/core/entidades/
        if (usuario.id) {
            url = this.url + usuario.id;
            method = RequestMethod.Put;
        }

        headers.append("Content-Type", "application/json");
        headers.append("Authorization", `Bearer ${Bodega.tokenManager.access_token}`);
        return this.http.request(url, { body: body, headers: headers, method: method }).map(x => x.json() as Usuario);
    }

    private handleError(error: any) {
        // In a real world app, we might use a remote logging infrastructure
        let errMsg = error.message || 'Server error';
        console.error('[UsuariosServicio]: ' + errMsg); // log to console instead
        return Observable.throw(errMsg);
    }
}