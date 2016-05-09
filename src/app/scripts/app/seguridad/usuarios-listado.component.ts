import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { UsuariosServicio } from "./usuarios.servicio"
import { UsuarioResumen } from "./modelos"
import { PaginaComponent } from "../pagina.component"

@Component({
    selector: 'usuarios-listado',
    templateUrl: 'app/seguridad/usuarios-listado.template.html',
    providers: [UsuariosServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent]
})
export class UsuariosListadoComponent implements OnInit {

    usuarios: UsuarioResumen[];

    constructor(private usuariosServicio: UsuariosServicio) {

    }

    ngOnInit() {
        this.usuariosServicio.obtenerTodos().subscribe(x => {
            this.usuarios = x;
            console.log(x);
        });
    }
}