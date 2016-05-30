import { Component, OnInit } from "@angular/core"
import { FORM_DIRECTIVES, CORE_DIRECTIVES } from "@angular/common"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { MarcasServicio } from "./marcas.servicio"
import { Marca } from "./marcas.modelos"
import { PaginacionResultado } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"
import { ErroresServicio } from "../errores.servicio"
/**
 * Componente que permite crear y editar bodegas desde la Interfaz de Usuario
 */
@Component({
    selector: 'marcas-editar',
    templateUrl: 'app/mantenimientos/marcas-editar.template.html',
    providers: [MarcasServicio, ErroresServicio],
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES, PaginaComponent, PaginacionComponent]
})

export class MarcasEditarComponent implements OnInit {
    private marcaId: number;
    public marca: Marca;
    public modoCreacion = false;
    public guardando = false;
    public mensaje: string;
    public alerta: string;
    public errores: string[];

    public constructor(
        private routeParams: RouteParams,
        private marcasServicio: MarcasServicio,
        private erroresServicio: ErroresServicio) {



        let idParam = this.routeParams.get("id");
        if (!idParam) {
            this.marcaId = null;
            this.modoCreacion = true;
        } else {
            this.marcaId = parseInt(idParam, 10);
            this.modoCreacion = false;
        }

    }

    ngOnInit() {
        this.obtenerMarca();
    }

    private obtenerMarca() {
        this.marca = {
            nombre: ""
        };
        
        if (!this.modoCreacion) {
            this.marcasServicio.obtenerUnica(this.marcaId).subscribe(x => {
                this.marca = x;
            });
        }
    }

    guardar() {
        this.guardando = true;
        this.mensaje = null;
        this.alerta = null;
        this.errores = null;

        this.marcasServicio.guardar(this.marca).subscribe(
            marca => {
                this.marcaId = marca.id;
                this.modoCreacion = false;
                this.marca = marca;
                this.guardando = false;
                this.mensaje = "la marca se guardó correctamente";
            },
            error => {
                this.guardando = false;
                if (this.erroresServicio.isNotModifiedResponse(error)) {
                    this.alerta = "Ninguna propiedad de la marca ha sido modificada.";
                } else {
                    this.errores = this.erroresServicio.obtenerErrores(error);
                }
            });
    }

}
