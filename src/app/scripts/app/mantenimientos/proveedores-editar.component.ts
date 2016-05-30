import { Component, OnInit } from "@angular/core"
import { FORM_DIRECTIVES, CORE_DIRECTIVES } from "@angular/common"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { ProveedoresServicio } from "./proveedores.servicio"
import { ProveedorDetalle } from "./proveedores.modelos"
import { PaginacionResultado } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"
import { ErroresServicio } from "../errores.servicio"

declare var $: any;
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
    private telefono: number;
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
                console.log("proveedor0");
                console.log(this.proveedor);
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

    public openAgregarTelefono() {
        this.telefono = null;
        $("#myModal").modal("show");
    }
    public ocultar() {
        if (this.modoCreacion) {
            return true;
        }
        else {
            return false
        }
    }

    public guardarTelefono() {
        this.guardando = true;
        this.mensaje = null;
        this.alerta = null;
        this.errores = null;
        console.log('entrada:' + this.telefono);
        this.proveedoresServicio.guardarTelefono(this.telefono, this.proveedorId).subscribe(
            telefono => {
                console.log(this.proveedor);
                this.proveedor.telefonos.push({ telefono: telefono.telefono });
                $("#myModal").modal("hide");
            });
    }

    public eliminarTelefono(telefono: number) {
        if (!confirm("Esta seguro de eliminar este teléfono?"))
            return;

        this.proveedoresServicio.eliminarTelefono(this.proveedor.id, telefono).subscribe(x => {
            for (var item of this.proveedor.telefonos) {
                if (item.telefono == telefono)
                    this.proveedor.telefonos.splice(this.proveedor.telefonos.indexOf(item), 1);
            }
        });
    }

}
