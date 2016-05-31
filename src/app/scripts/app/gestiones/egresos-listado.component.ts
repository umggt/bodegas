import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { EgresosServicio } from "./egresos.servicio"
import { OrdenarServicio} from "../ordenar.servicio"
import { EgresoResumen } from "./egresos.modelos"
import { PaginacionResultado, Dictionary } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"
import { ErroresServicio } from "../errores.servicio"
import { UnidadesDeMedidaServicio } from "../mantenimientos/unidades-de-medida.servicio"

@Component({
    selector: 'egresos-listado',
    templateUrl: 'app/gestiones/egresos-listado.template.html',
    providers: [EgresosServicio, ErroresServicio, UnidadesDeMedidaServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent, PaginacionComponent]
})

export class EgresosListadoComponent implements OnInit {

    private pagina: number = null;
    public egresos: PaginacionResultado<EgresoResumen>;
    public ordenar: OrdenarServicio;
    public errores: string[];

    constructor(private egresosServicio: EgresosServicio, private erroresServicio: ErroresServicio) {
        this.egresos = {};
        this.ordenar = new OrdenarServicio();
        this.ordenar.alCambiarOrden.subscribe(this.cambiarOrden);
    }

    public ngOnInit() {
        this.obtenerEgresos();
    }

    private obtenerEgresos(pagina?: number, campo?: string) {
        var ordenamiento = this.ordenar.campos;
        this.pagina = pagina;
        this.egresosServicio.obtenerTodos({ pagina: pagina, ordenamiento: ordenamiento }).subscribe(x => {
            this.egresos = x;
        });
    }

    public cambiarPagina(pagina: number) {
        this.obtenerEgresos(pagina);
    }

    public cambiarOrden = (columna: string) => {
        this.obtenerEgresos(this.pagina);
    }
}