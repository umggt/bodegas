import { Component, OnInit } from "@angular/core"
import { FORM_DIRECTIVES, CORE_DIRECTIVES } from "@angular/common"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { BodegasServicio } from "./bodegas.servicio"
import { BodegaResumen } from "./bodegas.modelos"
import { PaginacionResultado } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"
import { ErroresServicio } from "../errores.servicio"
/**
 * Componente que permite crear y editar bodegas desde la Interfaz de Usuario
 */
@Component({
    selector: 'bodegas-editar',
    templateUrl: 'app/mantenimientos/bodegas-editar.template.html',
    providers: [BodegasServicio, ErroresServicio],
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES, PaginaComponent, PaginacionComponent]
})

export class BodegasEditarComponent implements OnInit {
    private bodegaId: number;
    public bodega: BodegaResumen;
    public modoCreacion = false;
    public guardando = false;
    public mensaje: string;
    public alerta: string;
    public errores: string[];

    public constructor(
        private routeParams: RouteParams,
        private bodegasServicio: BodegasServicio,
        private erroresServicio: ErroresServicio) {

       

        let idParam = this.routeParams.get("id");
        if (!idParam) {
            this.bodegaId = null;
            this.modoCreacion = true;
        } else {
            this.bodegaId = parseInt(idParam, 10);
            this.modoCreacion = false;
        }

    }

    ngOnInit() {
        this.obtenerBodega();
    }

    private obtenerBodega() {
        this.bodega = {
            nombre: "",
            direccion: ""
        };
        console.log("modo creacion " + this.modoCreacion);
        if (!this.modoCreacion) {
            this.bodegasServicio.obtenerUnica(this.bodegaId).subscribe(x => {
                this.bodega = x;
                console.log(x);
            });
        }
    }

    guardar() {
        this.guardando = true;
        this.mensaje = null;
        this.alerta = null;
        this.errores = null;

        this.bodegasServicio.guardar(this.bodega).subscribe(
            bodega => {
                this.bodegaId = bodega.id;
                this.modoCreacion = false;
                this.bodega = bodega;
                this.guardando = false;
                this.mensaje = "la bodega se guardó correctamente";
            },
            error => {
                this.guardando = false;
                if (this.erroresServicio.isNotModifiedResponse(error)) {
                    this.alerta = "Ninguna propiedad de la bodega ha sido modificada.";
                } else {
                    this.errores = this.erroresServicio.obtenerErrores(error);
                }
            });
    }

}
