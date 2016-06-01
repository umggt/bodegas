import { Component, OnInit } from "@angular/core"
import { PaginaComponent } from "../pagina.component"
import { HttpServicio } from "../http.servicio"
import { DashboardServicio } from "./dashboard.servicio"
import { Resumen } from "./dashboard.modelos"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"

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
    }
}