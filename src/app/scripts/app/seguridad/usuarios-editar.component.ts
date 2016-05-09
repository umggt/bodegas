import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { UsuariosServicio } from "./usuarios.servicio"
import { Usuario } from "./modelos"
import { PaginaComponent } from "../pagina.component"

@Component({
    selector: 'usuarios-editar',
    templateUrl: 'app/seguridad/usuarios-editar.template.html',
    providers: [UsuariosServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent]
})
export class UsuariosEditarComponent implements OnInit {

    usuario: Usuario;

    constructor(private usuariosServicio: UsuariosServicio) {

    }

    getUsuario() {
        return JSON.stringify(this.usuario);
    }
    ngOnInit() {
        this.usuariosServicio.obtenerUnico(1).subscribe(x => {
            this.usuario = x;
            console.log(this.usuario);
        });
    }
}