//import { Component, OnInit } from "@angular/core"
//import { ROUTER_DIRECTIVES, RouteParams } from "@angular/router-deprecated"
//import { FORM_DIRECTIVES, CORE_DIRECTIVES } from "@angular/common"
//import { EgresosServicio } from "./egresos.servicio"
//import { OrdenarServicio} from "../ordenar.servicio"
//import { EgresoDetalle, Producto } from "./egresos.modelos"
//import { PaginaComponent } from "../pagina.component"
//import { ErroresServicio } from "../errores.servicio"
//import { PaginacionResultado } from "../modelos"
//import { UnidadDeMedida } from "../mantenimientos/unidades-de-medida.modelos"
//import { UnidadesDeMedidaServicio } from "./unidades-de-medida.servicio"

//declare var $: any;

//@Component({
//    selector: 'egresos-editar',
//    templateUrl: 'app/gestiones/egresos-editar.template.html',
//    providers: [EgresosServicio, ErroresServicio, UnidadesDeMedidaServicio],
//    directives: [ROUTER_DIRECTIVES, CORE_DIRECTIVES, FORM_DIRECTIVES, PaginaComponent]
//})

//export class EgresosEditarComponent implements OnInit
//{
//    private egresoId: number;

//    public modoCreacion: boolean;
//    public egreso: EgresoDetalle;
//    public guardando = false;
//    public mensaje: string;
//    public alerta: string;
//    public errores: string[];
//    public unidadesDeMedida: PaginacionResultado<UnidadDeMedida>;
//    public productos: Producto[];

//    public constructor(
//        private routeParams: RouteParams,
//        private egresosServicio: EgresosServicio,
//        private erroresServicio: ErroresServicio) {

//        let idParam = this.routeParams.get("id");
//        if (!idParam) {
//            this.productoId = null;
//            this.modoCreacion = true;
//        } else {
//            this.productoId = parseInt(idParam, 10);
//            this.modoCreacion = false;
//        }
//    }
//        )
//}