import { Component, OnInit } from "@angular/core"
import { OrdenarServicio} from "../ordenar.servicio"
import { IngresoResumen } from "./ingresos.modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionResultado } from "../modelos"
import { PaginacionComponent } from "../paginacion.component"

@Component({
    selector: 'listas-editar',
    templateUrl: 'app/gestiones/ingresos-listado.template.html',
    directives: [PaginaComponent, PaginacionComponent]
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