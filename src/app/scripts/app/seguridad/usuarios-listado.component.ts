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

    constructor(private usuariosServicio: UsuariosServicio) {
        this.usuarios = {};
        this.ordenar = new OrdenarServicio();
        this.ordenar.alCambiarOrden.subscribe(this.cambiarOrden);
    }

    ngOnInit() {
        this.obtenerUsuarios();
    }

    obtenerUsuarios(pagina?: number, campo?: string) {
        var ordenamiento = this.ordenar.campos;
        this.pagina = pagina;
        this.usuariosServicio.obtenerTodos({ pagina: pagina, ordenamiento: ordenamiento }).subscribe(x => {
            this.usuarios = x;
        });
    }

    cambiarPagina(pagina: number) {
        this.obtenerUsuarios(pagina);
    }

    cambiarOrden = (columna: string) => {
        this.obtenerUsuarios(this.pagina);
    }
}