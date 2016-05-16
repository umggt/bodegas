import { Component, OnInit } from "@angular/core"
import { FORM_DIRECTIVES, CORE_DIRECTIVES, NgForm } from "@angular/common"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { UsuariosServicio } from "./usuarios.servicio"
import { RolesServicio } from "./roles.servicio"
import { Usuario, RolResumen } from "./modelos"
import { PaginacionResultado } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"
import { ErroresServicio } from "../errores.servicio"

/**
 * Componente que permite crear y editar usuarios desde la Interfaz de Usuario
 */
@Component({
    selector: 'usuarios-editar',
    templateUrl: 'app/seguridad/usuarios-editar.template.html',
    providers: [UsuariosServicio, RolesServicio, ErroresServicio],
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES, PaginaComponent, PaginacionComponent]
})
export class UsuariosEditarComponent implements OnInit {

    private usuarioId: number;
    usuario: Usuario = {};
    roles: PaginacionResultado<RolResumen>;
    modoCreacion = false;
    guardando = false;
    mensaje: string;
    errores: string[];

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

        this.roles = {};

        let idParam = this.routeParams.get("id");
        if (!idParam) {
            this.usuarioId = null;
            this.modoCreacion = true;
        } else {
            this.usuarioId = parseInt(idParam, 10);
            this.modoCreacion = false;
        }
        
    }

    ngOnInit() {
        this.obtenerRoles();
        this.obtenerUsuario();
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

    /**
     * Obtiene los nombres de los atributos que tenga asignados el usuario.
     */
    atributos(): string[] {
        if (!this.usuario.atributos) return null;
        return Object.keys(this.usuario.atributos);
    }

    /**
     * Obtiene los valores de un atributo específico concatenados por comas ",".
     * @param atributo nombre del atributo
     */
    valores(atributo: string) : string {
        if (!this.usuario.atributos) return null;

        var atributoValor = this.usuario.atributos[atributo];

        if (!atributoValor) return null;

        return atributoValor.join(", ");
    }

    isInvalid(form: NgForm, control: string) {
        if (!form) return false;
        if (!form.controls) return false;
        if (!form.controls[control]) return false;
        var formControl = form.controls[control];
        return formControl.touched && !formControl.valid;
    }

    toggleRol(rol: RolResumen) {

        if (!this.usuario) return;

        var valor: string = null;
        if (!rol.asignado) {
            valor = rol.nombre;
        }

        this.usuario.roles[rol.id.toString()] = valor;
        rol.asignado = !rol.asignado;
    }

    guardar() {
        this.guardando = true;
        this.usuariosServicio.guardar(this.usuario).subscribe(
            usuario => {
                this.usuarioId = usuario.id;
                this.modoCreacion = false;
                this.usuario = usuario;
                this.guardando = false;
                this.mensaje = "el usuario se guardó correctamente";
            },
            error => {
                this.errores = this.erroresServicio.handleResponse(error);
                this.guardando = false;
            });
    }

    cambiarPaginaDeRol(pagina: number) {
        console.log(pagina);
    }

    /**
     * Obtiene la información del usuario y la almacena en el campo "usuario".
     */
    private obtenerUsuario() {
        if (this.modoCreacion) {
            this.usuario = {
                login: "",
                correo: "",
                nombres: "",
                apellidos: "",
                nombreCompleto: "",
                atributos: {},
                roles: {}
            };
        } else {
            this.usuariosServicio.obtenerUnico(this.usuarioId).subscribe(x => {
                this.usuario = x;
                this.checkRoles();
            });
        }
    }

    /**
     * Obtiene la información de los roles existentes en el sistema y almacena esta
     * información en el campo "roles".
     */
    private obtenerRoles() {
        this.rolesServicio.obtenerTodos().subscribe(x => {
            this.roles = x;
            this.checkRoles();
        });
    }

    private checkRoles() {

        if (this.modoCreacion) return;
        if (!this.roles || !this.roles.elementos) return;
        if (!this.usuario || !this.usuario.roles) return;

        for (var rol of this.roles.elementos) {
            rol.asignado = !!this.usuario.roles[rol.id.toString()];
        }
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
        var actualizar = nombreCompletoOriginal == `${nombresOriginales} ${apellidosOriginales}`.trim();

        this.usuario.nombres = nombres.trim();
        this.usuario.apellidos = apellidos.trim();

        if (actualizar) {
            this.usuario.nombreCompleto = `${nombres} ${apellidos}`.trim();
        }
    }

}