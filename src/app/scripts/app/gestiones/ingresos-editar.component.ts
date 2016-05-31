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
    public guardando: boolean;

    private datePicker: any;
    private productoEnEdicion: IngresoProducto;

    constructor(private ingresosServicio: IngresosServicio, private erroresServicio: ErroresServicio) {
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
        this.ingreso.fecha = this.datePicker.date().toDate() || new Date();

        this.guardando = true;
        this.ingresosServicio.guardar(this.ingreso).subscribe(
            ingreso => {
                this.mensaje = "El ingreso se guardó correctamente";
                this.datePicker.date(ingreso.fecha);
            },
            error => {
                this.errores = this.erroresServicio.obtenerErrores(error);
                this.guardando = false;
            });
    }

    public nuevoProducto() {
        this.producto = {};
        this.productoEnEdicion = null;
        this.caracteristicas = [];
        $("#producto-modal").modal("show");
    }

    public editarProducto(producto: IngresoProducto) {
        this.producto = this.crearCopia(producto);

        this.productoEnEdicion = producto;
        this.caracteristicas = [];
        this.seleccionarProducto();
        $("#producto-modal").modal("show");
    }

    private crearCopia(producto: IngresoProducto) {
        var copia: IngresoProducto = {
            productoId: producto.productoId,
            unidadId: producto.unidadId,
            nombre: producto.nombre,
            unidadNombre: producto.nombre,
            marcaId: producto.marcaId,
            serie: producto.serie,
            precio: producto.precio,
            cantidad: producto.cantidad,
            caracteristicas: []
        };


        if (producto.caracteristicas && producto.caracteristicas.length) {
            let caracteristicas = copia.caracteristicas;
            for (let c of producto.caracteristicas) {

                var ccopia: IngresoProductoCaracteristica = {
                    id: c.id,
                    nombre: c.nombre,
                    valor: c.valor,
                    listaValorId: c.listaValorId,
                    valores: [],
                    requerido: c.requerido,
                    minimo: c.minimo,
                    maximo: c.maximo,
                    esBooleano: c.esBooleano,
                    esLista: c.esLista,
                    esMoneda: c.esMoneda,
                    esNumero: c.esNumero,
                    esTextoCorto: c.esTextoCorto,
                    esTextoLargo: c.esTextoLargo
                };

                caracteristicas.push(ccopia);

                if (c.valores && c.valores.length) {
                    for (let v of c.valores) {
                        ccopia.valores.push({ id: v.id, valor: v.valor });
                    }
                }

            }
        }

        return copia;
    }

    public eliminarProducto(producto: IngresoProducto) {
        if (!confirm(`¿seguro que desea eliminar el producto ${producto.nombre}?`)) {
            return;
        }

        var productos = this.ingreso.productos;
        var index = productos.indexOf(producto);
        productos.splice(index, 1);
    }

    public cambiarProducto(event: string) {
        var productoId = parseInt(event, 10);
        this.producto.productoId = productoId;
        this.producto.nombre = this.obtenerProductoNombre(productoId);
        this.obtenerUnidades(productoId);
        this.obtenerMarcas(productoId);
        this.obtenerCaracteristicas(productoId);
    }

    public cambiarUnidad(event: string) {
        var unidadId = parseInt(event, 10);
        this.producto.unidadId = unidadId;
        this.producto.unidadNombre = this.obtenerUnidadNombre(unidadId);
    }

    private obtenerProductoNombre(productoId: number) {
        for (let p of this.productos.elementos) {
            if (p.id === productoId) return p.nombre;
        }

        return null;
    }

    private obtenerUnidadNombre(unidadId: number) {
        for (let u of this.unidades.elementos) {
            if (u.id === unidadId) return u.nombre;
        }

        return null;
    }

    public guardarProducto() {
        this.ingreso.productos = this.ingreso.productos || [];
        if (this.productoEnEdicion != null) {
            let index = this.ingreso.productos.indexOf(this.productoEnEdicion);

            var producto = this.crearCopia(this.producto);
            producto.caracteristicas = this.caracteristicas;

            this.ingreso.productos[index] = producto;
        } else {
            this.producto.caracteristicas = this.caracteristicas;
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
                    break;
                }
            }
        }

    }
   
}