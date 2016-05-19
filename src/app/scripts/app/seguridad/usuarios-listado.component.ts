import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { UsuariosServicio } from "./usuarios.servicio"
import { OrdenarServicio} from "../ordenar.servicio"
import { UsuarioResumen } from "./modelos"
import { PaginacionResultado, Dictionary } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"

@Component({
    selector: 'usuarios-listado',
    templateUrl: 'app/seguridad/usuarios-listado.template.html',
    providers: [UsuariosServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent, PaginacionComponent]
})
export class UsuariosListadoComponent implements OnInit {

    private pagina: number = null;
    usuarios: PaginacionResultado<UsuarioResumen>;
    ordenar: OrdenarServicio;

    constructor(private usuariosServicio: UsuariosServicio, ordenar: OrdenarServicio) {
        this.usuarios = {};
        this.ordenar = ordenar;
    }

    ngOnInit() {
        this.obtenerUsuarios();
    }

    obtenerUsuarios(pagina?: number, campo?: string) {
        var ord: Dictionary<boolean> = null;
        
        if (campo) {
            ord = {};
            ord[campo] = true;
        }

        this.pagina = pagina;
        this.usuariosServicio.obtenerTodos({ pagina: pagina, ordenamiento: ord }).subscribe(x => {
            this.usuarios = x;
        });
    }

    cambiarPagina(pagina: number) {
        this.obtenerUsuarios(pagina);
    }
    
}