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
    private marcasServicio: MarcasServicio, private bodegasServicio: BodegasServicio, private productosServicio: ProductosServicio, private unidadesServicio: UnidadesDeMedidaServicio) {
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
        this.obtenerProductos();
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
            productoId: producto.productoId,
            productoNombre: producto.productoNombre,
            unidadId: producto.unidadId,
            unidadNombre: producto.unidadNombre,
            cantidad: producto.cantidad
        };
        this.productoEnEdicion = producto;
        $("#producto-modal").modal("show");
    }

    public eliminarProducto(producto: Producto) {
        if (!confirm(`¿seguro que desea eliminar el producto ${producto.productoNombre}?`)) {
            return;
        }
    }

    public cambiarProducto(event: string) {
        var productoId = parseInt(event, 10);
        this.producto.productoId = productoId;
        this.producto.productoNombre = this.obtenerProductoNombre(productoId);

        this.obtenerUnidades(productoId);
    }

    public cambiarUnidad(event: string) {
        var unidadId = parseInt(event, 10);
        this.producto.unidadId = unidadId;
        this.producto.unidadNombre = this.obtenerUnidadNombre(unidadId);
    }

    private obtenerProductoNombre(productoId: number) : string {
        for (let p of this.productos.elementos) {
            if (p.id === productoId) {
                return p.nombre;
            }
        }

        return null;
    }

    public guardarProducto() {

        this.egreso.productos = this.egreso.productos || [];

        if (this.productoEnEdicion != null) {
            var p = this.productoEnEdicion;
            var pp = this.producto;

            p.productoId = pp.productoId;
            p.productoNombre = pp.productoNombre;
            p.unidadId = pp.unidadId;
            p.unidadNombre = pp.unidadNombre;
            p.cantidad = pp.cantidad;

        } else {
            this.egreso.productos.push(this.producto);
        }

        this.productoEnEdicion = null;
        this.producto = null;

        $("#producto-modal").modal("hide");
    }

    private obtenerProductos() {
        this.productosServicio.obtenerTodos().subscribe(x => {
            this.productos = x;
        });
    }

    private obtenerUnidades(productoId: number) {
        this.unidadesServicio.obtenerPorProducto(productoId).subscribe(x => {
            this.unidades = x;
        });
    }

    private obtenerUnidadNombre(unidadId: number) {
        for (let u of this.unidades.elementos) {
            if (u.id === unidadId) {
                return u.nombre;
            }
        }

        return null;
    }

    private obtenerBodegas() {

        this.bodegasServicio.obtenerTodas().subscribe(x => {
            this.bodegas = x;
        });
    }

    

    private obtenerCaracteristicas(productoId: number) {
    }
  
 }
