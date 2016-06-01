import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { OrdenarServicio} from "../ordenar.servicio"
import { IngresoResumen } from "./ingresos.modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionResultado } from "../modelos"
import { PaginacionComponent } from "../paginacion.component"
import { MarcasServicio } from "../mantenimientos/marcas.servicio"
import { UnidadesDeMedidaServicio } from "../mantenimientos/unidades-de-medida.servicio"
import { ListasServicio } from "../mantenimientos/listas.servicio"
import { ErroresServicio } from "../errores.servicio"
import { ProductosServicio } from "../mantenimientos/productos.servicio"
import { IngresosServicio } from "./ingresos.servicio"
import { ProveedoresServicio } from "../mantenimientos/proveedores.servicio"
import { BodegasServicio } from "../mantenimientos/bodegas.servicio"

@Component({
    selector: 'ingresos-listado',
    templateUrl: 'app/gestiones/ingresos-listado.template.html',
    providers: [ProductosServicio, ErroresServicio, MarcasServicio, UnidadesDeMedidaServicio, ListasServicio, IngresosServicio, ProveedoresServicio, BodegasServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent, PaginacionComponent]
})
export class IngresosListadoComponent implements OnInit {

    private pagina: number;
    public ordenar: OrdenarServicio;
    public ingresos: PaginacionResultado<IngresoResumen>;

    constructor(private ingresosServicio: IngresosServicio) {
        this.ingresos = {};
        this.ordenar = new OrdenarServicio();
        this.ordenar.alCambiarOrden.subscribe(this.cambiarOrden);
    }

    public ngOnInit() {
        this.obtenerIngresos();
    }

    private obtenerIngresos(pagina?: number) {
        var ordenamiento = this.ordenar.campos;
        this.pagina = pagina;
        this.ingresosServicio.obtenerTodos({ pagina: pagina, ordenamiento: ordenamiento }).subscribe(x => {
            this.ingresos = x;
        });
    }

    public cambiarPagina(pagina: number) {
        this.obtenerIngresos(pagina);
    }

    public cambiarOrden(columna: string) {
        this.obtenerIngresos(this.pagina);
    }

    public fecha(fechaStr: string) {
        return new Date(fechaStr);
    }

}