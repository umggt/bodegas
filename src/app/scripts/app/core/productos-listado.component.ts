import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { ProductosServicio } from "./productos.servicio"
import { OrdenarServicio} from "../ordenar.servicio"
import { ProductoResumen } from "./productos.modelos"
import { PaginacionResultado, Dictionary } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"

@Component({
    selector: 'productos-listado',
    templateUrl: 'app/core/productos-listado.template.html',
    providers: [ProductosServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent, PaginacionComponent]
})
export class ProductosListadoComponent implements OnInit {

    private pagina: number = null;
    productos: PaginacionResultado<ProductoResumen>;
    ordenar: OrdenarServicio;

    constructor(private productosServicio: ProductosServicio) {
        this.productos = {};
        this.ordenar = new OrdenarServicio();
        this.ordenar.alCambiarOrden.subscribe(this.cambiarOrden);
    }

    ngOnInit() {
        this.obtenerProductos();
    }

    obtenerProductos(pagina?: number) {
        var ordenamiento = this.ordenar.campos;
        this.pagina = pagina;
        this.productosServicio.obtenerTodos({ pagina: pagina, ordenamiento: ordenamiento }).subscribe(x => {
            this.productos = x;
        });
    }

    cambiarPagina(pagina: number) {
        this.obtenerProductos(pagina);
    }

    cambiarOrden = (columna: string) => {
        this.obtenerProductos(this.pagina);
    }
}