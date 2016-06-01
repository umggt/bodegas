import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
import { FORM_DIRECTIVES, CORE_DIRECTIVES } from "@angular/common"
import { ListasServicio } from "./listas.servicio"
import { OrdenarServicio} from "../ordenar.servicio"
import { Lista } from "./listas.modelos"
import { PaginaComponent } from "../pagina.component"
import { ErroresServicio } from "../errores.servicio"

declare var $: any;

@Component({
    selector: 'listas-editar',
    templateUrl: 'app/mantenimientos/listas-editar.template.html',
    providers: [ListasServicio, ErroresServicio],
    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES, PaginaComponent]
})
export class ListasEditarComponent implements OnInit {

    private listaId: number;
    private valor: string;

    public modoCreacion: boolean;
    public lista: Lista;
    public guardando = false;
    public mensaje: string;
    public alerta: string;
    public errores: string[];

    public constructor(
        private routeParams: RouteParams,
        private listasServicio: ListasServicio,
        private erroresServicio: ErroresServicio) {

        let idParam = this.routeParams.get("id");
        if (!idParam) {
            this.listaId = null;
            this.modoCreacion = true;
        } else {
            this.listaId = parseInt(idParam, 10);
            this.modoCreacion = false;
        }
    }


    public ngOnInit() {
        this.obtenerLista();
    }
    

    private obtenerLista() {
        this.lista = {
            nombre: ""            
        }
        if (!this.modoCreacion) {
            this.listasServicio.obtenerUnica(this.listaId).subscribe(x => {
                this.lista = x;
            });
        }
    }

    public guardar() {
        this.guardando = true;
        this.mensaje = null;
        this.alerta = null;
        this.errores = null;

        this.listasServicio.guardar(this.lista).subscribe(
            lista => {
                this.listaId = lista.id;
                this.modoCreacion = false;
                this.lista = lista;
                this.guardando = false;
                this.mensaje = "la lista se guardó correctamente";
            },
            error => {
                this.guardando = false;
                if (this.erroresServicio.isNotModifiedResponse(error)) {
                    this.alerta = "Ninguna propiedad de la lista ha sido modificada.";
                } else {
                    this.errores = this.erroresServicio.obtenerErrores(error);
                }
            });
    }
    public mostrar()
    {        
        if (this.lista.id)
            return false;

        return true;
    }

    public nuevo()
    {       
        this.valor = "";
        $("#myModal").modal("show");
    }

    public guardarValor()
    {
        this.guardando = true;
        this.mensaje = null;
        this.alerta = null;
        this.errores = null;
        console.log('entrada:' + this.valor);
        this.listasServicio.guardarValor(this.valor, this.listaId).subscribe(
            valor => {
                this.lista.valores.push({ id: valor.id, valor: valor.valor });
                $("#myModal").modal("hide");
            });
    }

    public eliminarValor(idValor: number) {       
        if (!confirm("Esta seguro de eliminar este valor?"))
            return;

        this.listasServicio.eliminarValor(this.lista.id, idValor).subscribe(x => {
            for (var item of this.lista.valores) {
                if (item.id == idValor)
                    this.lista.valores.splice(this.lista.valores.indexOf(item), 1);
            }
        });
    }

}