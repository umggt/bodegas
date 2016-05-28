import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { BodegasServicio } from "./bodegas.servicio"
import { OrdenarServicio} from "../ordenar.servicio"
import { BodegaResumen } from "./modelos"
import { PaginacionResultado, Dictionary } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"

@Component({
    selector: 'bodegas-listado',
    templateUrl: 'app/mantenimientos/bodegas-listado.template.html',
    providers: [BodegasServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent, PaginacionComponent]
})
export class BodegasListadoComponent implements OnInit {
    private pagina: number = null;
    bodegas: PaginacionResultado<BodegaResumen>;
    ordenar: OrdenarServicio;

    constructor(private bodegasServicio: BodegasServicio) {
        this.bodegas = {};
        this.ordenar = new OrdenarServicio();
        this.ordenar.alCambiarOrden.subscribe(this.cambiarOrden);
    }

    ngOnInit() {
        this.obtenerBodegas();
    }

    obtenerBodegas(pagina?: number, campo?: string) {
        var ordenamiento = this.ordenar.campos;
        this.pagina = pagina;
        this.bodegasServicio.obtenerTodas({ pagina: pagina, ordenamiento: ordenamiento }).subscribe(x => {
            this.bodegas = x;
        })
    }
    cambiarPagina(pagina: number) {
        this.obtenerBodegas(pagina);
    }

    cambiarOrden = (columna: string) => {
        this.obtenerBodegas(this.pagina);
    }

}