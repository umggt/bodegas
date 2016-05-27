import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { ListasServicio } from "./listas.servicio"
import { OrdenarServicio} from "../ordenar.servicio"
import { ListaResumen } from "./modelos"
import { PaginacionResultado, Dictionary } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"

@Component({
    selector: 'listas-listado',
    templateUrl: 'app/mantenimientos/listas-listado.template.html',
    providers: [ListasServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent, PaginacionComponent]
})
export class ListasListadoComponent implements OnInit {

    private pagina: number = null;
    listas: PaginacionResultado<ListaResumen>;
    ordenar: OrdenarServicio;

    constructor(private listasServicio: ListasServicio) {
        this.listas = {};
        this.ordenar = new OrdenarServicio();
        this.ordenar.alCambiarOrden.subscribe(this.cambiarOrden);
    }

    ngOnInit() {
        this.obtenerListas();
    }

    obtenerListas(pagina?: number, campo?: string) {
        var ordenamiento = this.ordenar.campos;
        this.pagina = pagina;
        this.listasServicio.obtenerTodas({ pagina: pagina, ordenamiento: ordenamiento }).subscribe(x => {
            this.listas = x;
        });
    }

    cambiarPagina(pagina: number) {
        this.obtenerListas(pagina);
    }

    cambiarOrden = (columna: string) => {
        this.obtenerListas(this.pagina);
    }
}