import { Component } from "@angular/core"
import { RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from "@angular/router-deprecated"
import { HTTP_BINDINGS, HTTP_PROVIDERS } from "@angular/http"
import { BodegasListadoComponent } from "./core/bodegas-listado.component"
import { NavegacionComponent } from "./core/navegacion.component"
import { DashboardComponent } from "./core/dashboard.component"
import { UsuariosListadoComponent } from "./seguridad/usuarios-listado.component"
import { UsuariosEditarComponent } from "./seguridad/usuarios-editar.component"
import { ProductosListadoComponent } from "./core/productos-listado.component"
import { ProductosEditarComponent } from "./core/productos-editar.component"

@Component({
    selector: 'bodegas-main',
    templateUrl: 'app/main.template.html',
    directives: [ROUTER_DIRECTIVES, NavegacionComponent],
    providers: [ROUTER_PROVIDERS, HTTP_PROVIDERS]
})
@RouteConfig([
    {
        path: '/',
        name: 'root',
        redirectTo: ['/Dashboard'],
        useAsDefault: true
    },
    {
        path: '/dashboard',
        name: 'Dashboard',
        component: DashboardComponent
    },
    {
        path: '/bodegas',
        name: 'BodegasListado',
        component: BodegasListadoComponent
    },
    {
        path: '/usuarios',
        name: 'UsuariosListado',
        component: UsuariosListadoComponent
    },
    {
        path: '/usuarios/nuevo',
        name: 'UsuariosCrear',
        component: UsuariosEditarComponent
    },
    {
        path: '/usuarios/:id',
        name: 'UsuariosEditar',
        component: UsuariosEditarComponent
    },
    {
        path: '/productos',
        name: 'ProductosListado',
        component: ProductosListadoComponent
    },
    {
        path: '/productos/nuevo',
        name: 'ProductosCrear',
        component: ProductosEditarComponent
    },
    {
        path: '/productos/:id',
        name: 'ProductosEditar',
        component: ProductosEditarComponent
    }
])
export class MainComponent {

}