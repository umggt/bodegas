import { Component } from "angular2/core"
import { RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from "angular2/router"
import { HTTP_BINDINGS } from "angular2/http"
import { BodegasListadoComponent } from "./core/bodegas-listado.component"

@Component({
    selector: 'bodegas-main',
    templateUrl: 'app/main.template.html',
    directives: [ROUTER_DIRECTIVES],
    providers: [ROUTER_PROVIDERS]
})
@RouteConfig([
    {
        path: '/bodegas',
        name: 'BodegasListado',
        component: BodegasListadoComponent
    }
])
export class MainComponent {

}