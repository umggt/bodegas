import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { PaginaComponent } from "../pagina.component"
import { Traslado } from "./traslados.modelos"
import { Producto } from "./egresos.modelos"
import { PaginacionResultado } from "../modelos"
import { BodegaResumen } from "../mantenimientos/bodegas.modelos"
import { ProductoResumen } from "../mantenimientos/productos.modelos"
import { UnidadDeMedida } from "../mantenimientos/unidades-de-medida.modelos"
import { Marca } from "../mantenimientos/marcas.modelos"
import { MarcasServicio } from "../mantenimientos/marcas.servicio"
import { BodegasServicio } from "../mantenimientos/bodegas.servicio"
import { UnidadesDeMedidaServicio } from "../mantenimientos/unidades-de-medida.servicio"
import { ErroresServicio } from "../errores.servicio"
import { TrasladosServicio } from "./traslados.servicio"
import { ProductosServicio } from "../mantenimientos/productos.servicio"
import { ListasServicio } from "../mantenimientos/listas.servicio"


declare var $: any;

@Component({
    selector: 'traslados-editar',
    templateUrl: 'app/gestiones/traslados-editar.template.html',
    providers: [TrasladosServicio, ErroresServicio, MarcasServicio, UnidadesDeMedidaServicio, BodegasServicio, ProductosServicio, ListasServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent]
})
export class TrasladosEditarComponent implements OnInit {

    private pagina: number = null;
    public errores: string[];
    public mensaje: string;
    public traslado: Traslado;
    public bodegas: PaginacionResultado<BodegaResumen>;
    public producto: Producto;
    public productos: PaginacionResultado<ProductoResumen>;
    public marcas: PaginacionResultado<Marca>;
    public unidades: PaginacionResultado<UnidadDeMedida>;
    public fechaTxt: string;
    private datePicker: any;
    private productoEnEdicion: Producto;
    public guardando: boolean;


    constructor(private erroresServicio: ErroresServicio, private marcasServicio: MarcasServicio, private bodegasServicio: BodegasServicio, private productosServicio: ProductosServicio, private unidadesServicio: UnidadesDeMedidaServicio, private trasladosServicio: TrasladosServicio) {
        this.traslado = {};
        this.bodegas = {};
        this.productos = {};
        this.marcas = {};
        this.unidades = {}
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


    public nuevo() {
        this.datePicker.date(new Date());
        this.traslado = {};
        this.guardando = false;
    }

    public nuevoProducto() {
        this.producto = {};
        this.productoEnEdicion = null;

        $("#producto-modal").modal("show");
    }

    public editarProducto(producto: Producto) {
        this.producto = this.crearCopia(producto);
        this.productoEnEdicion = producto;
        this.seleccionarProducto();
        $("#producto-modal").modal("show");
    }

    public cambiarProducto(event: string) {
        var productoId = parseInt(event, 10);
        this.producto.productoId = productoId;
        this.producto.productoNombre = this.obtenerProductoNombre(productoId);
        this.obtenerMarcas(productoId);
        this.obtenerUnidades(productoId);
    }

    public cambiarUnidad(event: string) {
        var unidadId = parseInt(event, 10);
        this.producto.unidadId = unidadId;
        this.producto.unidadNombre = this.obtenerUnidadNombre(unidadId);
    }

    public eliminarProducto(producto: Producto) {
        if (!confirm(`¿seguro que desea eliminar el producto ${producto.productoNombre} del traslado?`)) {
            return;
        }

        var productos = this.traslado.productos || [];
        var index = productos.indexOf(producto);
        if (index !== -1) {
            productos.splice(index, 1);
        }
    }

    public guardarProducto() {

        this.producto.productoNombre = this.obtenerProductoNombre(this.producto.productoId);
        this.producto.marcaNombre = this.obtenerMarcaNombre(this.producto.marcaId);
        this.producto.unidadNombre = this.obtenerUnidadNombre(this.producto.unidadId);

        this.traslado.productos = this.traslado.productos || [];
        if (this.productoEnEdicion != null) {
            let index = this.traslado.productos.indexOf(this.productoEnEdicion);

            var producto = this.crearCopia(this.producto);


            this.traslado.productos[index] = producto;
        } else {

            this.traslado.productos.push(this.producto);
        }

        this.productoEnEdicion = null;
        this.producto = null;

        $("#producto-modal").modal("hide");
    }

    public guardar() {
        this.traslado.fecha = this.datePicker.date().toDate();
        this.guardando = true;
        this.trasladosServicio.guardar(this.traslado).subscribe(
            traslado => {
                this.mensaje = "El traslado se guardó correctamente";
                this.datePicker.date(traslado.fecha);
            },
            error => {
                this.errores = this.erroresServicio.obtenerErrores(error);
                this.guardando = false;
            });
    }

    private obtenerProductos() {
        this.productosServicio.obtenerTodos().subscribe(x => {
            this.productos = x;
        });
    }

    private obtenerBodegas() {

        this.bodegasServicio.obtenerTodas().subscribe(x => {
            this.bodegas = x;
        });
    }

    private obtenerUnidades(productoId: number) {
        this.unidadesServicio.obtenerPorProducto(productoId).subscribe(x => {
            this.unidades = x;
        });
    }

    private obtenerMarcas(productoId: number) {
        this.marcasServicio.obtenerPorProducto(productoId).subscribe(x => {
            this.marcas = x;
        });
    }

    private seleccionarProducto() {
        if (!this.producto || !this.producto.productoId) return;
        if (!this.productos || !this.productos.elementos || !this.productos.elementos.length) return;
        this.cambiarProducto(this.producto.productoId.toString());
    }

    private obtenerProductoNombre(productoId: number): string {
        for (let p of this.productos.elementos) {
            if (p.id === productoId) {
                return p.nombre;
            }
        }

        return null;
    }

    private obtenerUnidadNombre(unidadId: number) {
        for (let u of this.unidades.elementos) {
            if (u.id === unidadId) {
                return u.nombre;
            }
        }

        return null;
    }

    private obtenerMarcaNombre(marcaId: number) {
        for (let m of this.marcas.elementos) {
            if (m.id === marcaId) {
                return m.nombre;
            }
        }

        return null;
    }

    private crearCopia(producto: Producto) {
        var copia: Producto = {
            productoId: producto.productoId,
            unidadId: producto.unidadId,
            marcaId: producto.marcaId,
            cantidad: producto.cantidad,
            productoNombre: producto.productoNombre,
            unidadNombre: producto.unidadNombre,
            marcaNombre: producto.marcaNombre
        };

        return copia;
    }

}