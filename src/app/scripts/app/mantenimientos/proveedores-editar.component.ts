import { Component, OnInit } from "@angular/core"
import { FORM_DIRECTIVES, CORE_DIRECTIVES } from "@angular/common"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { ProveedoresServicio } from "./proveedores.servicio"
import { ProveedorDetalle } from "./proveedores.modelos"
import { PaginacionResultado } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"
import { ErroresServicio } from "../errores.servicio"


@Component({
    selector: 'proveedores-editar',
    templateUrl: 'app/mantenimientos/proveedores-editar.template.html',
    providers: [ProveedoresServicio, ErroresServicio],
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES, PaginaComponent, PaginacionComponent]
})

export class ProveedoresEditarComponent implements OnInit {
    private proveedorId: number;
    public proveedor: ProveedorDetalle;
    public modoCreacion = false;
    public guardando = false;
    public mensaje: string;
    public alerta: string;
    public errores: string[];

    public constructor(
        private routeParams: RouteParams,
        private proveedoresServicio: ProveedoresServicio,
        private erroresServicio: ErroresServicio) {

        let idParam = this.routeParams.get("id");
        if (!idParam) {
            this.proveedorId = null;
            this.modoCreacion = true;
        } else {
            this.proveedorId = parseInt(idParam, 10);
            this.modoCreacion = false;
        }

    }

    ngOnInit() {
        this.obtenerProveedor();
    }

    private obtenerProveedor() {
        this.proveedor = {
            nombre: "",
            direccion: "",
            nombreDeContacto: ""
        };
        console.log("modo creacion " + this.modoCreacion);
        if (!this.modoCreacion) {
            this.proveedoresServicio.obtenerUnico(this.proveedorId).subscribe(x => {
                this.proveedor = x;
            });
        }
    }

    guardar() {
        this.guardando = true;
        this.mensaje = null;
        this.alerta = null;
        this.errores = null;

        this.proveedoresServicio.guardar(this.proveedor).subscribe(
            proveedor => {
                this.proveedorId = proveedor.id;
                this.modoCreacion = false;
                this.proveedor = proveedor;
                this.guardando = false;
                this.mensaje = "el proveedor se guardó correctamente";
            },
            error => {
                this.guardando = false;
                if (this.erroresServicio.isNotModifiedResponse(error)) {
                    this.alerta = "Ninguna propiedad del proveedor ha sido modificada.";
                } else {
                    this.errores = this.erroresServicio.obtenerErrores(error);
                }
            });
    }

}
