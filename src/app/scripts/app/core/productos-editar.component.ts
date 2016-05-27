import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { FORM_DIRECTIVES, CORE_DIRECTIVES } from "@angular/common"
import { ProductosServicio } from "./productos.servicio"
import { OrdenarServicio} from "../ordenar.servicio"
import { Producto } from "./productos.modelos"
import { PaginaComponent } from "../pagina.component"
import { ErroresServicio } from "../errores.servicio"

@Component({
    selector: 'productos-editar',
    templateUrl: 'app/core/productos-editar.template.html',
    providers: [ProductosServicio, ErroresServicio],
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES, PaginaComponent]
})
export class ProductosEditarComponent implements OnInit {

    private productoId: number;

    public modoCreacion: boolean;
    public producto: Producto;
    public guardando = false;
    public mensaje: string;
    public alerta: string;
    public errores: string[];

    public constructor(
        private routeParams: RouteParams,
        private productosServicio: ProductosServicio,
        private erroresServicio: ErroresServicio) {

        let idParam = this.routeParams.get("id");
        if (!idParam) {
            this.productoId = null;
            this.modoCreacion = true;
        } else {
            this.productoId = parseInt(idParam, 10);
            this.modoCreacion = false;
        }
    }


    public ngOnInit() {
        this.obtenerProducto();
    }

    public guardar() {
        this.guardando = true;
        this.mensaje = null;
        this.alerta = null;
        this.errores = null;

        this.productosServicio.guardar(this.producto).subscribe(
            producto => {
                this.productoId = producto.id;
                this.modoCreacion = false;
                this.producto = producto;
                this.guardando = false;
                this.mensaje = "el producto se guardó correctamente";
            },
            error => {
                this.guardando = false;
                if (this.erroresServicio.isNotModifiedResponse(error)) {
                    this.alerta = "Ninguna propiedad del producto ha sido modificada.";
                } else {
                    this.errores = this.erroresServicio.obtenerErrores(error);
                }
            });
    }

    private obtenerProducto() {
        this.producto = {
            nombre: "",
            descripcion: ""
        }
        if (!this.modoCreacion) {
            this.productosServicio.obtenerUnico(this.productoId).subscribe(x => {
                this.producto = x;
            });
        }
    }


}