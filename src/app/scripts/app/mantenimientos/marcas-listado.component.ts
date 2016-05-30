import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { MarcasServicio } from "./marcas.servicio"
import { OrdenarServicio} from "../ordenar.servicio"
import { Marca } from "./marcas.modelos"
import { PaginacionResultado, Dictionary } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"

@Component({
    selector: 'marcas-listado',
    templateUrl: 'app/mantenimientos/marcas-listado.template.html',
    providers: [MarcasServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent, PaginacionComponent]
})
export class MarcasListadoComponent implements OnInit {
    private pagina: number = null;
    marcas: PaginacionResultado<Marca>;
    ordenar: OrdenarServicio;

    constructor(private marcasServicio: MarcasServicio) {
        this.marcas = {};
        this.ordenar = new OrdenarServicio();
        this.ordenar.alCambiarOrden.subscribe(this.cambiarOrden);
    }

    ngOnInit() {
        this.obtenerMarcas();
    }

    obtenerMarcas(pagina?: number, campo?: string) {
        var ordenamiento = this.ordenar.campos;
        this.pagina = pagina;
        this.marcasServicio.obtenerTodos({ pagina: pagina, ordenamiento: ordenamiento }).subscribe(x => {
            this.marcas = x;
        })
    }
    cambiarPagina(pagina: number) {
        this.obtenerMarcas(pagina);
    }

    cambiarOrden = (columna: string) => {
        this.obtenerMarcas(this.pagina);
    }

}