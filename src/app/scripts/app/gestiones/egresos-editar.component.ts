import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { PaginaComponent } from "../pagina.component"
import { EgresoDetalle, Producto } from "./egresos.modelos"
import { PaginacionResultado } from "../modelos"
import { BodegaResumen } from "../mantenimientos/bodegas.modelos"
import { ProductoResumen } from "../mantenimientos/productos.modelos"
import { UnidadDeMedida } from "../mantenimientos/unidades-de-medida.modelos"
import { Marca } from "../mantenimientos/marcas.modelos"
import { MarcasServicio } from "../mantenimientos/marcas.servicio"
import { BodegasServicio } from "../mantenimientos/bodegas.servicio"
import { UnidadesDeMedidaServicio } from "../mantenimientos/unidades-de-medida.servicio"
import { ErroresServicio } from "../errores.servicio"
import { EgresosServicio } from "./egresos.servicio"
import { ProductosServicio } from "../mantenimientos/productos.servicio"
import { ListasServicio } from "../mantenimientos/listas.servicio"

declare var $: any;

@Component({
    selector: 'egresos-editar',
    templateUrl: 'app/gestiones/egresos-editar.template.html',
    providers: [EgresosServicio, ErroresServicio, MarcasServicio, UnidadesDeMedidaServicio, BodegasServicio, ProductosServicio, ListasServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent]
})
export class EgresosEditarComponent implements OnInit {
    private pagina: number = null;
    public egresoId: number;
    public errores: string[];
    public mensaje: string;
    public egreso: EgresoDetalle;
    public bodegas: PaginacionResultado<BodegaResumen>;
    public producto: Producto;
    public productos: PaginacionResultado<ProductoResumen>;
    public marcas: PaginacionResultado<Marca>;
    public unidades: PaginacionResultado<UnidadDeMedida>;
    public fechaTxt: string;
    public modoCreacion: boolean;
    private datePicker: any;
    private productoEnEdicion: Producto;

    constructor(
       
    private routeParams: RouteParams,
    private egresosServicio: EgresosServicio,
    private erroresServicio: ErroresServicio,
    private marcasServicio: MarcasServicio, private bodegasServicio: BodegasServicio, private productosServicio: ProductosServicio) {
        this.egreso = {
            fecha: new Date()
        };
        let idParam = this.routeParams.get("id");
        if (!idParam) {
            this.egresoId = null;
            this.modoCreacion = true;
        } else {
            this.egresoId = parseInt(idParam, 10);
            this.modoCreacion = false;
        }
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
        this.obtenerBodegas();
    }

    public guardar() {
        this.egreso.fecha = this.datePicker.date().toDate();
    }

    public nuevoProducto() {
        this.producto = {};
        this.productoEnEdicion = null;
        
        $("#producto-modal").modal("show");
    }

    public editarProducto(producto: Producto) {
        this.producto = {
            nombre: producto.nombre,
            unidadDeMedidaNombre: producto.unidadDeMedidaNombre,
            cantidad: producto.cantidad
        };
        this.productoEnEdicion = producto;
        $("#producto-modal").modal("show");
    }

    public eliminarProducto(producto: Producto) {
        if (!confirm(`¿seguro que desea eliminar el producto ${producto.nombre}?`)) {
            return;
        }
    }

    public cambiarProducto(event: string) {
        var productoId = parseInt(event, 10);
        this.producto.id = productoId;
    }

    public guardarProducto() {
    }

    private obtenerProductos(pagina?: number, campo?: string) {
        this.pagina = pagina;
        this.productosServicio.obtenerTodos({ pagina: pagina }).subscribe(x => {
            this.productos = x;
        })
    }

    private obtenerUnidades() {
    }

    obtenerBodegas(pagina?: number, campo?: string) {
       
        this.pagina = pagina;
        this.bodegasServicio.obtenerTodas({ pagina: pagina}).subscribe(x => {
            this.bodegas = x;
        })
    }

    

    private obtenerCaracteristicas(productoId: number) {
    }
  
 }
