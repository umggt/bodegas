import { Component } from "angular2/core"
import { MenuSuperiorComponent } from "./menu-superior.component"
import { MenuPrincipalComponent } from "./menu-principal.component"

@Component({
    selector: 'navegacion',
    templateUrl: 'app/core/navegacion.template.html',
    directives: [MenuSuperiorComponent, MenuPrincipalComponent]
})
export class NavegacionComponent {

}