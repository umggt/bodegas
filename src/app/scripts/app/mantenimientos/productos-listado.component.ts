import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { ProductosServicio } from "./productos.servicio"
import { OrdenarServicio} from "../ordenar.servicio"
import { ProductoResumen } from "./productos.modelos"
import { PaginacionResultado, Dictionary } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"
import { ErroresServicio } from "../errores.servicio"
import { MarcasServicio } from "./marcas.servicio"
import { UnidadesDeMedidaServicio } from "./unidades-de-medida.servicio"
import { ListasServicio } from "./listas.servicio"

@Component({
    selector: 'productos-listado',
    templateUrl: 'app/mantenimientos/productos-listado.template.html',
    providers: [ProductosServicio, ErroresServicio, MarcasServicio, UnidadesDeMedidaServicio, ListasServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent, PaginacionComponent]
})
export class ProductosListadoComponent implements OnInit {

    private pagina: number = null;
    public productos: PaginacionResultado<ProductoResumen>;
    public ordenar: OrdenarServicio;
    public errores: string[];

    constructor(private productosServicio: ProductosServicio, private erroresServicio: ErroresServicio) {
        this.productos = {};
        this.ordenar = new OrdenarServicio();
        this.ordenar.alCambiarOrden.subscribe(this.cambiarOrden);
    }

    public ngOnInit() {
        this.obtenerProductos();
    }

    private obtenerProductos(pagina?: number) {
        var ordenamiento = this.ordenar.campos;
        this.pagina = pagina;
        this.productosServicio.obtenerTodos({ pagina: pagina, ordenamiento: ordenamiento }).subscribe(x => {
            this.productos = x;
        });
    }

    public cambiarPagina(pagina: number) {
        this.obtenerProductos(pagina);
    }

    public cambiarOrden = (columna: string) => {
        this.actualizarPaginaActual();
    }

    public eliminar(producto: ProductoResumen) {
        if (!confirm(`¿Seguro que desea eliminar el producto ${producto.nombre}`)) {
            return;
        }
        
        this.productosServicio.eliminar(producto.id).subscribe(
            this.eliminarSuccess,
            this.eliminarError,
            this.eliminarComplete
        );
    }

    private eliminarSuccess = () => {
        this.actualizarPaginaActual();
    }

    private eliminarError = (error: any) => {
        if (this.erroresServicio.isNotModifiedResponse(error)) {
            this.errores = ["Ningún producto fué eliminado."];
        } else {
            this.errores = this.erroresServicio.obtenerErrores(error);
        }
    }

    private eliminarComplete = () => {
        this.errores = [];
    }

    private actualizarPaginaActual() {
        this.obtenerProductos(this.pagina);
    }

}