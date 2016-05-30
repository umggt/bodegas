import { Component, OnInit } from "@angular/core"
import { FORM_DIRECTIVES, CORE_DIRECTIVES } from "@angular/common"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { UnidadesDeMedidaServicio } from "./unidades-de-medida.servicio"
import { UnidadDeMedida } from "./unidades-de-medida.modelos"
import { PaginacionResultado } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"
import { ErroresServicio } from "../errores.servicio"
/**
 * Componente que permite crear y editar bodegas desde la Interfaz de Usuario
 */
@Component({
    selector: 'unidades-de-medida-editar',
    templateUrl: 'app/mantenimientos/unidades-de-medida-editar.template.html',
    providers: [UnidadesDeMedidaServicio, ErroresServicio],
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES, PaginaComponent, PaginacionComponent]
})

export class UnidadesDeMedidaEditarComponent implements OnInit {
    private unidadMedidaId: number;
    public unidadMedida: UnidadDeMedida;
    public modoCreacion = false;
    public guardando = false;
    public mensaje: string;
    public alerta: string;
    public errores: string[];

    public constructor(
        private routeParams: RouteParams,
        private unidadesDeMedidaServicio: UnidadesDeMedidaServicio,
        private erroresServicio: ErroresServicio) {



        let idParam = this.routeParams.get("id");
        if (!idParam) {
            this.unidadMedidaId = null;
            this.modoCreacion = true;
        } else {
            this.unidadMedidaId = parseInt(idParam, 10);
            this.modoCreacion = false;
        }

    }

    ngOnInit() {
        this.obtenerUnidadDeMedida();
    }

    private obtenerUnidadDeMedida() {
        this.unidadMedida = {
            nombre: ""
        };

        if (!this.modoCreacion) {
            this.unidadesDeMedidaServicio.obtenerUnica(this.unidadMedidaId).subscribe(x => {
                this.unidadMedida = x;
            });
        }
    }

    guardar() {
        this.guardando = true;
        this.mensaje = null;
        this.alerta = null;
        this.errores = null;

        this.unidadesDeMedidaServicio.guardar(this.unidadMedida).subscribe(
            UnidadDeMedida => {
                this.unidadMedidaId = UnidadDeMedida.id;
                this.modoCreacion = false;
                this.unidadMedida = UnidadDeMedida;
                this.guardando = false;
                this.mensaje = "la unidad de medida se guardó correctamente";
            },
            error => {
                this.guardando = false;
                if (this.erroresServicio.isNotModifiedResponse(error)) {
                    this.alerta = "Ninguna propiedad de la unidad de medida ha sido modificada.";
                } else {
                    this.errores = this.erroresServicio.obtenerErrores(error);
                }
            });
    }

}
