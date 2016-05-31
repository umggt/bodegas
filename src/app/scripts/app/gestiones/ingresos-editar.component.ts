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
        this.producto = this.deepCopy(producto);
        
        this.productoEnEdicion = producto;
        this.caracteristicas = [];
        this.seleccionarProducto();
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
        this.obtenerUnidades(productoId);
        this.obtenerMarcas(productoId);
        this.obtenerCaracteristicas(productoId);
    }

    public guardarProducto() {
        this.ingreso.productos = this.ingreso.productos || [];
        if (this.productoEnEdicion != null) {
            let index = this.ingreso.productos.indexOf(this.productoEnEdicion);
            this.ingreso.productos[index] = this.deepCopy(this.producto);
        } else {
            this.ingreso.productos.push(this.producto);
        }

        this.productoEnEdicion = null;
        this.producto = null;
        $("#producto-modal").modal("hide");
    }

    private obtenerProductos() {
        this.ingresosServicio.obtenerProductos().subscribe(x => {
            this.productos = x;
            this.seleccionarProducto();
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
        this.ingresosServicio.obtenerCaracteristicas(productoId).subscribe(x => {
            this.caracteristicas = x;
            this.seleccionarCaracteristicas();
        });
    }

    private seleccionarProducto() {
        if (!this.producto || !this.producto.productoId) return;
        if (!this.productos || !this.productos.elementos || !this.productos.elementos.length) return;
        this.cambiarProducto(this.producto.productoId.toString());
    }
    
    private seleccionarCaracteristicas() {
        if (!this.producto || !this.producto.caracteristicas || !this.producto.caracteristicas.length) return;
        if (!this.caracteristicas || !this.caracteristicas.length) return;

        for (let pc of this.producto.caracteristicas) {
            for (let c of this.caracteristicas) {
                if (pc.id === c.id) {
                    if (pc.esBooleano) {
                        c.valor = new Boolean(pc.valor);
                    } else if (pc.esNumero) {
                        c.valor = parseFloat(pc.valor);
                    } else {
                        c.valor = pc.valor;
                    }
                    c.listaValorId = pc.listaValorId;
                    return;
                }
            }
        }

    }

    private deepCopy(obj: Object | Object[]) {
        if (Object.prototype.toString.call(obj) === '[object Array]') {
            let out = Array, i = 0, len = (obj as Object[]).length;
            for (; i < len; i++) {
                out[i] = arguments.callee(obj[i]);
            }
            return out;
        }
        if (typeof obj === 'object') {
            let out = {}, i: any;
            for (i in obj) {
                out[i] = arguments.callee(obj[i]);
            }
            return out;
        }
        return obj;
    }
}