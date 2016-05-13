import { Component, OnInit } from "@angular/core"
import { FORM_DIRECTIVES, CORE_DIRECTIVES }    from "@angular/common"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { UsuariosServicio } from "./usuarios.servicio"
import { Usuario } from "./modelos"
import { PaginaComponent } from "../pagina.component"

@Component({
    selector: 'usuarios-editar',
    templateUrl: 'app/seguridad/usuarios-editar.template.html',
    providers: [UsuariosServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent, CORE_DIRECTIVES, FORM_DIRECTIVES]
})
export class UsuariosEditarComponent implements OnInit {

    private usuarioId: number;
    usuario: Usuario = {};

    constructor(private routeParams: RouteParams, private usuariosServicio: UsuariosServicio) {
        this.usuarioId = parseInt(routeParams.get("id"), 10);
    }

    getUsuario() {
        return JSON.stringify(this.usuario);
    }

    ngOnInit() {
        
        this.usuariosServicio.obtenerUnico(this.usuarioId).subscribe(x => {
            this.usuario = x;
            console.log(this.usuario);
        });
    }
}