import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { OrdenarServicio} from "../ordenar.servicio"
import { IngresoResumen } from "./ingresos.modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionResultado } from "../modelos"
import { PaginacionComponent } from "../paginacion.component"

@Component({
    selector: 'ingresos-listado',
    templateUrl: 'app/gestiones/ingresos-listado.template.html',
    directives: [ROUTER_DIRECTIVES, PaginaComponent, PaginacionComponent]
})
export class IngresosListadoComponent implements OnInit {

    public ordenar: OrdenarServicio;
    public ingresos: PaginacionResultado<IngresoResumen>;

    constructor() {
        this.ordenar = new OrdenarServicio();
        this.ingresos = {};
    }

    public ngOnInit() {
        
    }

}