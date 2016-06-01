import { Component, OnInit } from "@angular/core"
import { PaginaComponent } from "../pagina.component"
import { HttpServicio } from "../http.servicio"
import { DashboardServicio } from "./dashboard.servicio"
import { Resumen } from "./dashboard.modelos"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"

declare var $: any;
declare var Morris: any;

@Component({
    selector: 'dashboard',
    templateUrl: 'app/core/dashboard.template.html',
    providers: [HttpServicio, DashboardServicio],
    directives: [PaginaComponent, ROUTER_DIRECTIVES]
})
export class DashboardComponent implements OnInit {

    public resumen: Resumen;

    constructor(private dashboardServicio: DashboardServicio) {
        this.resumen = {};
    }

    public ngOnInit() {
        this.dashboardServicio.obtenerResumen().subscribe(x => {
            this.resumen = x;
        });
        this.dashboardServicio.obtenerIngresosVsEgresos().subscribe(x => {
            Morris.Line({
                element: 'ingresos-vs-egresos',
                data: x,
                xkey: 'fecha',
                ykeys: ['ingresos', 'egresos'],
                labels: ['Ingresos', 'Egresos'],
                dateFormat(milliseconds: number): string {
                    var fecha = new Date(milliseconds);
                    var dia = fecha.getDate().toString();
                    var mes = fecha.getMonth().toString();
                    var anio = fecha.getFullYear();
                    dia = dia.length == 1 ? `0${dia}` : dia;
                    mes = mes.length == 1 ? `0${mes}` : mes;
                    return `${dia}/${mes}/${anio}`;
                }
            });
        })
    }
}