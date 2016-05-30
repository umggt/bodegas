import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { FORM_DIRECTIVES, CORE_DIRECTIVES } from "@angular/common"
import { ProductosServicio } from "./productos.servicio"
import { OrdenarServicio} from "../ordenar.servicio"
import { Producto, Caracteristica, TipoCaracteristica } from "./productos.modelos"
import { PaginaComponent } from "../pagina.component"
import { ErroresServicio } from "../errores.servicio"
import { PaginacionResultado } from "../modelos"
import { Marca } from "./marcas.modelos"
import { UnidadDeMedida } from "./unidades-de-medida.modelos"
import { MarcasServicio } from "./marcas.servicio"
import { UnidadesDeMedidaServicio } from "./unidades-de-medida.servicio"
import { ListaResumen } from "./listas.modelos"
import { ListasServicio } from "./listas.servicio"

declare var $: any;

@Component({
    selector: 'productos-editar',
    templateUrl: 'app/mantenimientos/productos-editar.template.html',
    providers: [ProductosServicio, ErroresServicio, MarcasServicio, UnidadesDeMedidaServicio, ListasServicio],
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
    public marcas: PaginacionResultado<Marca>;
    public unidadesDeMedida: PaginacionResultado<UnidadDeMedida>;
    public caracteristica: Caracteristica;
    public tiposCaracteristica: TipoCaracteristica[];
    public listas: ListaResumen[];
    public esLista: boolean = false;
    public esNumero: boolean = false;
    private caracteristicaEnEdicion: Caracteristica;

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
        this.obtenerMarcas();
        this.obtenerUnidadesDeMedida();
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
        this.producto = {}

        if (!this.modoCreacion) {
            this.productosServicio.obtenerUnico(this.productoId).subscribe(x => {
                this.producto = x;
                this.seleccionarMarcas();
                this.seleccionarUnidades();
            });
        }
    }

    private obtenerMarcas() {
        this.productosServicio.obtenerMarcas().subscribe(x => {
            this.marcas = x;
            this.seleccionarMarcas();
        });
    }

    private obtenerUnidadesDeMedida() {
        this.productosServicio.obtenerUnidadesDeMedida().subscribe(x => {
            this.unidadesDeMedida = x;
            this.seleccionarUnidades();
        });
    }

    private seleccionarMarcas() {
        if (!this.producto || !this.producto.marcas || !this.producto.marcas.length) {
            return;
        }

        if (!this.marcas || !this.marcas.elementos || !this.marcas.elementos.length) {
            return;
        }

        for (let marca of this.marcas.elementos) {
            marca.asignado = this.producto.marcas.indexOf(marca.id) !== -1;
        }
    }

    private seleccionarUnidades() {
        if (!this.producto || !this.producto.unidades || !this.producto.unidades.length) {
            return;
        }

        if (!this.unidadesDeMedida || !this.unidadesDeMedida.elementos || !this.unidadesDeMedida.elementos.length) {
            return;
        }

        for (let unidad of this.unidadesDeMedida.elementos) {
            unidad.asignado = this.producto.unidades.indexOf(unidad.id) !== -1;
        }
    }

    public toggleUnidad(item: UnidadDeMedida) {
        item.asignado = !item.asignado;

        this.producto.unidades = this.producto.unidades || [];

        var index = this.producto.unidades.indexOf(item.id);
        var existe = index !== -1;

        if (item.asignado && !existe) {
            this.producto.unidades.push(item.id);
        } else if (!item.asignado && existe) {
            this.producto.unidades.splice(index, 1);
        }
    }

    public toggleMarca(item: Marca) {
        item.asignado = !item.asignado;

        this.producto.marcas = this.producto.marcas || [];

        var index = this.producto.marcas.indexOf(item.id);
        var existe = index !== -1;

        if (item.asignado && !existe) {
            this.producto.marcas.push(item.id);
        } else if (!item.asignado && existe) {
            this.producto.marcas.splice(index, 1);
        }

    }

    public guardarCaracteristica() {
        this.producto.caracteristicas = this.producto.caracteristicas || [];

        if (!this.caracteristicaEnEdicion) {
            this.producto.caracteristicas.push(this.caracteristica);
            this.caracteristica = null;
        } else {
            let a = this.caracteristicaEnEdicion;
            let b = this.caracteristica;

            a.nombre = b.nombre;
            a.tipo = b.tipo;
            a.tipoNombre = b.tipoNombre;
            a.listaId = b.listaId;
            a.minimo = b.minimo;
            a.maximo = b.maximo;
            a.requerido = b.requerido;
            a.expresion = b.expresion;

            this.caracteristicaEnEdicion = null;
            this.caracteristica = null;
        }
        $("#carcteristicas-modal").modal("hide");
    }

    public nuevaCaracteristica() {
        this.caracteristica = {
            nombre: null,
            tipo: 0,
            tipoNombre: "",
            requerido: false,
            listaId: null,
            minimo: null,
            maximo: null,
            expresion: null
        };
        this.caracteristicaEnEdicion = null;
        this.abrirModalEdicionCaracteristica();
    }

    private abrirModalEdicionCaracteristica() {
        if (!this.tiposCaracteristica || !this.tiposCaracteristica.length) {
            this.productosServicio.obtenerCaracteristicas().subscribe(x => {
                this.tiposCaracteristica = x;
                this.cambiarTipo(x[0].id.toString());
            });
        } else {
            this.cambiarTipo(this.tiposCaracteristica[0].id.toString());
        }

        if (!this.listas || !this.listas.length) {
            this.productosServicio.obtenerListas().subscribe(x => {
                this.listas = x.elementos;
                this.caracteristica.listaId = this.listas[0].id;
            });
        } else {
            this.caracteristica.listaId = this.listas[0].id;
        }

        $("#carcteristicas-modal").modal("show");
    }

    public cambiarTipo(eventData: string) {
        var tipo = parseInt(eventData, 10);
        this.caracteristica.tipo = tipo;

        for (let t of this.tiposCaracteristica) {
            if (t.id === tipo) {
                this.caracteristica.tipoNombre = t.nombre;
                break;
            }
        }

        this.esLista = tipo === 8 || tipo === 9;
        this.esNumero = [0, 1, 2, 3, 4].indexOf(tipo) >= 0;
    }

    public eliminarCaracteristica(item: Caracteristica) {
        if (!confirm(`¿seguro que desea eliminar la característica ${item.nombre}`)){
            return;
        }

        var index = this.producto.caracteristicas.indexOf(item);
        this.producto.caracteristicas.splice(index, 1);
    }

    public editarCaracteristica(item: Caracteristica) {
        this.caracteristica = {
            id: item.id,
            tipo: item.tipo,
            tipoNombre: item.tipoNombre,
            nombre: item.nombre,
            listaId: item.listaId,
            minimo: item.minimo,
            maximo: item.maximo,
            requerido: item.requerido,
            expresion: item.expresion
        };
        this.caracteristicaEnEdicion = item;
        this.abrirModalEdicionCaracteristica();
    }
}