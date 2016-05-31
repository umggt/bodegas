import { Component } from "@angular/core"
import { RouteConfig, ROUTER_DIRECTIVES, ROUTER_PROVIDERS } from "@angular/router-deprecated"
import { HTTP_BINDINGS, HTTP_PROVIDERS } from "@angular/http"
import { BodegasListadoComponent } from "./mantenimientos/bodegas-listado.component"
import { BodegasEditarComponent } from "./mantenimientos/bodegas-editar.component"
import { NavegacionComponent } from "./core/navegacion.component"
import { DashboardComponent } from "./core/dashboard.component"
import { UsuariosListadoComponent } from "./seguridad/usuarios-listado.component"
import { UsuariosEditarComponent } from "./seguridad/usuarios-editar.component"
import { ProductosListadoComponent } from "./mantenimientos/productos-listado.component"
import { ProductosEditarComponent } from "./mantenimientos/productos-editar.component"
import { ListasListadoComponent } from "./mantenimientos/listas-listado.component"
import { ListasEditarComponent } from "./mantenimientos/listas-editar.component"
import { ProveedoresListadoComponent } from "./mantenimientos/proveedores-listado.component"
import { ProveedoresEditarComponent } from "./mantenimientos/proveedores-editar.component"
import { MarcasListadoComponent } from "./mantenimientos/marcas-listado.component"
import { MarcasEditarComponent } from "./mantenimientos/marcas-editar.component"
import { UnidadesDeMedidaListadoComponent } from "./mantenimientos/unidades-de-medida-listado.component"
import { UnidadesDeMedidaEditarComponent } from "./mantenimientos/unidades-de-medida-editar.component"
import { IngresosListadoComponent } from "./gestiones/ingresos-listado.component"
import { IngresosEditarComponent } from "./gestiones/ingresos-editar.component"

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
        path: '/bodegas/nuevo',
        name: 'BodegasCrear',
        component: BodegasEditarComponent
    },

    {
        path: '/bodegas/:id',
        name: 'BodegasEditar',
        component: BodegasEditarComponent
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
    },
    {
        path: '/listas',
        name: 'ListasListado',
        component: ListasListadoComponent
    },
    {
        path: '/listas/nuevo',
        name: 'ListasCrear',
        component: ListasEditarComponent
    },
    {
        path: '/listas/:id',
        name: 'ListasEditar',
        component: ListasEditarComponent
    },

     {
        path: '/proveedores',
        name: 'ProveedoresListado',
        component: ProveedoresListadoComponent
    },
     {
         path: '/proveedores/nuevo',
         name: 'ProveedoresCrear',
         component: ProveedoresEditarComponent
     },

     {
         path: '/proveedores/:id',
         name: 'ProveedoresEditar',
         component: ProveedoresEditarComponent
     },

     {
         path: '/marcas',
         name: 'MarcasListado',
         component: MarcasListadoComponent
     },
     {
         path: '/marcas/nuevo',
         name: 'MarcasCrear',
         component: MarcasEditarComponent
     },

     {
         path: '/marcas/:id',
         name: 'MarcasEditar',
         component: MarcasEditarComponent
     },
     {
         path: '/unidadesDeMedida',
         name: 'UnidadesDeMedidaListado',
         component: UnidadesDeMedidaListadoComponent
     },
     {
         path: '/unidadesDeMedida/nuevo',
         name: 'UnidadesDeMedidaCrear',
         component: UnidadesDeMedidaEditarComponent
     },
     {
         path: '/unidadesDeMedida/:id',
         name: 'UnidadesDeMedidaEditar',
         component: UnidadesDeMedidaEditarComponent
     },
     {
         path: '/ingresos',
         name: 'IngresosListado',
         component: IngresosListadoComponent
     },
     {
        path: '/ingresos/nuevo',
        name: 'IngresosCrear',
        component: IngresosEditarComponent
     },
     {
         path: '/egresos',
         name: 'EgresosListado',
         component: ProductosListadoComponent
     },
     {
         path: '/traslados',
         name: 'TrasladosListado',
         component: ProductosListadoComponent
     }
])
export class MainComponent {

}