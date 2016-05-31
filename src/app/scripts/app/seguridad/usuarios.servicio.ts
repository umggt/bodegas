import { Injectable } from "@angular/core"
import { Observable } from 'rxjs/Observable'
import { Headers, RequestMethod, URLSearchParams } from "@angular/http"
import { UsuarioResumen, Usuario, Contrasenias } from "./modelos"
import { PaginacionResultado, PaginacionParametros } from "../modelos"
import { HttpServicio } from "../http.servicio"

@Injectable()
export class UsuariosServicio {

    private url = "http://localhost:5002/api/core/usuarios/";

    constructor(private http: HttpServicio) { }

    obtenerTodos(paginacion?: PaginacionParametros): Observable<PaginacionResultado<UsuarioResumen>> {
        var params = this.http.params(paginacion);
        return this.http.get(this.url, { search: params }).map(x => x.json() as PaginacionResultado<UsuarioResumen>);
    }

    obtenerUnico(id: number): Observable<Usuario> {        
        return this.http.get(this.url + id).map(x => x.json() as Usuario);        
    }

    guardar(usuario: Usuario): Observable<Usuario> {
        const body = JSON.stringify(usuario);
        let url = this.url;
        let method = RequestMethod.Post;

        // Si la entidad tiene un Id, se hace un HTTP PUT a /api/core/entidades/id
        // en lugar de un HTTP POST a /api/core/entidades/
        if (usuario.id) {
            url = this.url + usuario.id;
            method = RequestMethod.Put;
        }

        return this.http.request(url, { body: body, method: method }).map(x => x.json() as Usuario);
    }

    cambiarContrasenia(claves: Contrasenias): Observable<Contrasenias>{
        let url = this.url + 'perfil';
        const body = JSON.stringify(claves);
        let method = RequestMethod.Put;

        
        return this.http.request(url, { body: body, method: method }).map(x => x.json() as Contrasenias);
    }

    private handleError(error: any) {
        // In a real world app, we might use a remote logging infrastructure
        let errMsg = error.message || 'Server error';
        console.error('[UsuariosServicio]: ' + errMsg); // log to console instead
        return Observable.throw(errMsg);
    }
}