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
import { MarcasServicio } from "../mantenimientos/marcas.servicio"
import { UnidadesDeMedidaServicio } from "../mantenimientos/unidades-de-medida.servicio"
import { ListasServicio } from "../mantenimientos/listas.servicio"
import { ErroresServicio } from "../errores.servicio"
import { ProductosServicio } from "../mantenimientos/productos.servicio"
import { IngresosServicio } from "./ingresos.servicio"
import { ProveedoresServicio } from "../mantenimientos/proveedores.servicio"
import { BodegasServicio } from "../mantenimientos/bodegas.servicio"

declare var $: any;

@Component({
    selector: 'ingresos-editar',
    templateUrl: 'app/gestiones/ingresos-editar.template.html',
    providers: [ProductosServicio, ErroresServicio, MarcasServicio, UnidadesDeMedidaServicio, ListasServicio, IngresosServicio, ProveedoresServicio, BodegasServicio],
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
    public fechaTxt: string;

    private datePicker: any;
    private productoEnEdicion: IngresoProducto;

    constructor(private ingresosServicio: IngresosServicio) {
        this.ingreso = {
            fecha: new Date()
        };
        this.proveedores = {};
        this.bodegas = {};
    }

    public ngOnInit() {
        $("#input-fecha").datetimepicker({
            locale: 'es',
            format: 'DD/MM/YYYY hh:mm a',
            allowInputToggle: true
        });
        this.datePicker = $('#input-fecha').data("DateTimePicker");
        this.datePicker.date(new Date());
        this.obtenerProveedores();
        this.obtenerBodegas();
        this.obtenerProductos();
    }

    public guardar() {
        this.ingreso.fecha = this.datePicker.date().toDate();
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
        //this.caracteristicas = [];
        this.obtenerUnidades(productoId);
        this.obtenerMarcas(productoId);
    }

    public guardarProducto() {
    }

    private obtenerProductos() {
        this.ingresosServicio.obtenerProductos().subscribe(x => {
            this.productos = x;
        });
    }

    private obtenerProveedores() {
        this.ingresosServicio.obtenerProveedores().subscribe(x => {
            this.proveedores = x;
        });
    }

    private obtenerBodegas() {
        this.ingresosServicio.obtenerBodegas().subscribe(x => {
            this.bodegas = x;
        })
    }

    private obtenerUnidades(productoId: number) {
        this.ingresosServicio.obtenerUnidades(productoId).subscribe(x => {
            this.unidades = x;
        });
    }

    private obtenerMarcas(productoId: number) {
        this.ingresosServicio.obtenerMarcas(productoId).subscribe(x => {
            this.marcas = x;
        });
    }

    private obtenerCaracteristicas(productoId: number) {

    }
}