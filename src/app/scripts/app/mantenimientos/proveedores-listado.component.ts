import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { ProveedoresServicio } from "./proveedores.servicio"
import { OrdenarServicio} from "../ordenar.servicio"
import { ProveedorResumen } from "./proveedores.modelos"
import { PaginacionResultado, Dictionary } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"

@Component({
    selector: 'proveedores-listado',
    templateUrl: 'app/mantenimientos/proveedores-listado.template.html',
    providers: [ProveedoresServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent, PaginacionComponent]
})

export class ProveedoresListadoComponent implements OnInit {
    private pagina: number = null;
    proveedores: PaginacionResultado<ProveedorResumen>;
    ordenar: OrdenarServicio;

    constructor(private proveedoresServicio: ProveedoresServicio) {
        this.proveedores = {};
        this.ordenar = new OrdenarServicio();
        this.ordenar.alCambiarOrden.subscribe(this.cambiarOrden);
    }

    ngOnInit() {
        this.ObtenerProveedores();
    }

    ObtenerProveedores(pagina?: number, campo?: string) {
        var ordenamiento = this.ordenar.campos;
        this.pagina = pagina;
        this.proveedoresServicio.obtenerTodos({ pagina: pagina, ordenamiento: ordenamiento }).subscribe(x => {
            this.proveedores = x;
        })
    }
    cambiarPagina(pagina: number) {
        this.ObtenerProveedores(pagina);
    }

    cambiarOrden = (columna: string) => {
        this.ObtenerProveedores(this.pagina);
    }

}