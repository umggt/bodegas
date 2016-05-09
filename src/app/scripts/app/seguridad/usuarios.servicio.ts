import { Injectable } from "@angular/core"
import { Http, Headers, RequestMethod } from "@angular/http"
import { UsuarioResumen, Usuario } from "./modelos"

@Injectable()
export class UsuariosServicio {

    private url = "http://localhost:5002/api/core/usuarios/";

    constructor(private http: Http) { }

    obtenerTodos() {
        const headers = new Headers();
        headers.append("Authorization", `Bearer ${Bodega.tokenManager.access_token}`);

        return this.http.get(this.url, { headers: headers }).map(x => x.json() as UsuarioResumen[]);
    }

    obtenerUnico(id: number) {
        const headers = new Headers();
        headers.append("Authorization", `Bearer ${Bodega.tokenManager.access_token}`);
        return this.http.get(this.url + id, { headers: headers }).map(x => x.json() as Usuario);
    }

    guardar(usuario: Usuario) {
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
}