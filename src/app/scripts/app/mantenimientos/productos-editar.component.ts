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

    private obtenerMarcas() {
        this.productosServicio.obtenerMarcas().subscribe(x => {
            this.marcas = x;
        });
    }

    private obtenerUnidadesDeMedida() {
        this.productosServicio.obtenerUnidadesDeMedida().subscribe(x => {
            this.unidadesDeMedida = x;
        });
    }

    

    public toggle(item: any) {
        item.asignado = !item.asignado;
    }

    public guardarCaracteristica() {
        this.producto.caracteristicas = this.producto.caracteristicas || [];
        this.producto.caracteristicas.push(this.caracteristica);
        this.caracteristica = null;
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
    
}