import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { UsuariosServicio } from "./usuarios.servicio"
import { UsuarioResumen } from "./modelos"
import { PaginacionResultado } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"

@Component({
    selector: 'usuarios-listado',
    templateUrl: 'app/seguridad/usuarios-listado.template.html',
    providers: [UsuariosServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent, PaginacionComponent]
})
export class UsuariosListadoComponent implements OnInit {

    usuarios: PaginacionResultado<UsuarioResumen>;

    constructor(private usuariosServicio: UsuariosServicio) {
        this.usuarios = { };
    }

    ngOnInit() {
        this.usuariosServicio.obtenerTodos().subscribe(x => {
            this.usuarios = x;
        });
    }

    cambiarPagina(pagina: number) {
        console.log(pagina);
    }
}