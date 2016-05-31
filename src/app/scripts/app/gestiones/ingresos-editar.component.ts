import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { PaginaComponent } from "../pagina.component"
import { IngresoDetalle, IngresoProducto, IngresoProductoCaracteristica } from "./ingresos.modelos"
import { PaginacionResultado } from "../modelos"
import { ProveedorResumen } from "../mantenimientos/proveedores.modelos"
import { BodegaResumen } from "../mantenimientos/bodegas.modelos"
import { ProductoResumen } from "../mantenimientos/productos.modelos"
import { UnidadDeMedida } from "../mantenimientos/unidades-de-medida.modelos"
import { Marca } from "../mantenimientos/marcas.modelos"
 
declare var $: any;

@Component({
    selector: 'ingresos-editar',
    templateUrl: 'app/gestiones/ingresos-editar.template.html',
    directives: [ROUTER_DIRECTIVES, PaginaComponent]
})
export class IngresosEditarComponent implements OnInit {

    public errores: string[];
    public mensaje: string;
    public ingreso: IngresoDetalle;
    public proveedores: PaginacionResultado<ProveedorResumen>;
    public bodegas: PaginacionResultado<BodegaResumen>;
    public producto: IngresoProducto;
    public productos: PaginacionResultado<ProductoResumen>;
    public unidades: PaginacionResultado<UnidadDeMedida>;
    public marcas: PaginacionResultado<Marca>;
    public caracteristicas: IngresoProductoCaracteristica[];

    private productoEnEdicion: IngresoProducto;

    constructor() {
        this.ingreso = {
            fecha: new Date()
        };
        this.proveedores = {};
        this.bodegas = {};
    }

    public ngOnInit() {

    }

    public guardar() {

    }

    public nuevoProducto() {
        this.producto = {};
        this.productoEnEdicion = null;
        this.caracteristicas = [];
        $("#producto-modal").modal("show");
    }

    public editarProducto(producto: IngresoProducto) {
        this.producto = {
            nombre: producto.nombre,
            unidadNombre: producto.unidadNombre,
            cantidad: producto.cantidad
        };
        this.productoEnEdicion = producto;
        this.caracteristicas = [];
        $("#producto-modal").modal("show");
    }

    public eliminarProducto(producto: IngresoProducto) {
        if (!confirm(`¿seguro que desea eliminar el producto ${producto.nombre}?`)) {
            return;
        }
    }

    public cambiarProducto(event: string) {
        var productoId = parseInt(event, 10);
        this.producto.productoId = productoId;
        this.caracteristicas = [];
    }

    public guardarProducto() {
    }

    private obtenerProductos() {
    }

    private obtenerUnidades() {
    }

    private obtenerMarcas() {
    }

    private obtenerCaracteristicas(productoId: number) {
    }
}