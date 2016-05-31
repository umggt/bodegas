import { Component, OnInit } from "@angular/core"
import { FORM_DIRECTIVES, CORE_DIRECTIVES } from "@angular/common"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { UsuariosServicio } from "./usuarios.servicio"
import { RolesServicio } from "./roles.servicio"
import { Usuario, RolResumen, Contrasenias } from "./modelos"
import { PaginacionResultado } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"
import { ErroresServicio } from "../errores.servicio"

declare var $: any;

/**
 * Componente que permite crear y editar usuarios desde la Interfaz de Usuario
 */
@Component({
    selector: 'perfil-editar',
    templateUrl: 'app/seguridad/perfil-editar.template.html',
    providers: [UsuariosServicio, RolesServicio, ErroresServicio],
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES, PaginaComponent, PaginacionComponent]
})
export class PerfilEditarComponent implements OnInit {

    private usuarioId: number;
    usuario: Usuario = {};
    roles: PaginacionResultado<RolResumen>;
    guardando = false;
    mensaje: string;
    alerta: string;
    errores: string[];
    erroresModal: string[];
    private actual: string;
    private nueva: string;
    private confirma: string;
    alertaContrasenia: string;
    

    /**
     * Constructor del componente
     * @param routeParams servicio que permite acceder a los parámetros de la url.
     * @param usuariosServicio servicio que permite acceder a la api REST de usuarios.
     */
    constructor(
        private routeParams: RouteParams,
        private usuariosServicio: UsuariosServicio,
        private rolesServicio: RolesServicio,
        private erroresServicio: ErroresServicio) {

        let idParam = this.routeParams.get("id");
        //if (!idParam) {
        //    this.usuarioId = null;
        //    this.modoCreacion = true;
        //} else {
        //    this.usuarioId = parseInt(idParam, 10);
        //    this.modoCreacion = false;
        //}        
    }

    ngOnInit() {
        this.obtenerUsuario();
    }

    /**
   * Obtiene la información del usuario y la almacena en el campo "usuario".
   */
    private obtenerUsuario() {       
            this.usuario = {
                login: "",
                correo: "",
                nombres: "",
                apellidos: "",
                nombreCompleto: ""               
        };
        this.usuariosServicio.obtenerUnico(0).subscribe(x => {
            this.usuario = x;
            });
    }

    /**
     * Cambia el valor de los nombres del usuario (actualizando el nombre completo si aplica).
     * @param nombres
     */
    cambiarNombres(nombres: string) {
        var apellidos = this.usuario.apellidos;
        this.cambiarNombreCompleto(nombres, apellidos);
    }

    /**
     * Cambia el valor de los apellidos del usuario (actualizando el nombre completo si aplica).
     * @param apellidos
     */
    cambiarApellidos(apellidos : string) {
        var nombres = this.usuario.nombres;
        this.cambiarNombreCompleto(nombres, apellidos);
    }


    guardar() {
        this.guardando = true;
        this.mensaje = null;
        this.alerta = null;
        this.errores = null;        

        this.usuariosServicio.guardar(this.usuario).subscribe(
            usuario => {
                this.usuarioId = 0;               
                this.usuario = usuario;
                this.guardando = false;
                this.mensaje = "el usuario se guardó correctamente";
            },
            error => {
                this.guardando = false;
                if (this.erroresServicio.isNotModifiedResponse(error)) {
                    this.alerta = "Ninguna propiedad del usuario ha sido modificada.";
                } else {
                    this.errores = this.erroresServicio.obtenerErrores(error);
                }
            });
    }

    /**
     * Verifica si el nombre completo es equivalente a la concatenación de
     * nombres y apellidos, de ser así se asume que cualquier cambio en los
     * nombres o apellidos actualiza el nombre completo. Pero si el nombre
     * completo es distinto, se asume que debe quedar tal y como está.
     * @param nombres el nuevo valor para los nombres
     * @param apellidos el nuevo valor para los apellidos
     */
    private cambiarNombreCompleto(nombres: string, apellidos: string) {
        var nombreCompletoOriginal = this.usuario.nombreCompleto;
        var nombresOriginales = this.usuario.nombres;
        var apellidosOriginales = this.usuario.apellidos;
        var actualizar = !nombreCompletoOriginal || nombreCompletoOriginal == `${nombresOriginales} ${apellidosOriginales}`;

        this.usuario.nombres = nombres;
        this.usuario.apellidos = apellidos;

        if (actualizar) {
            let nuevoNombreCompleto = `${nombres} ${apellidos}`;
            this.usuario.nombreCompleto = nuevoNombreCompleto.trim() ? nuevoNombreCompleto : "";
        }
    }

    cancelar()
    {        
        window.history.go(-2);
    }

    mostrarModal() { 
        this.actual = "";
        this.nueva = "";
        this.confirma = "";       
        $("#myModal").modal("show");
    }

    cambiarContrasenia() {
        this.alertaContrasenia = null;
        
        if (this.nueva != this.confirma) {
            this.alertaContrasenia = "La contraseña nueva no coincide con la confirmación";
            return;
        }        
        let claves: Contrasenias = {};
        claves.actual = this.actual;
        claves.nueva = this.nueva;

        let resultado: string;
        
        if(this.actual && this.nueva)
            this.usuariosServicio.cambiarContrasenia(claves).subscribe(
                success => {
                    this.mensaje = "Contraseña modificada correctamente";
                    $("#myModal").modal("hide");
                },
                error => {
                    this.erroresModal = this.erroresServicio.obtenerErrores(error);
                }
            );

    }
}