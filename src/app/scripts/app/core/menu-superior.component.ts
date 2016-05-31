import { Component } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"

@Component({
    selector: 'menu-superior',
    templateUrl: 'app/core/menu-superior.template.html',
    directives: [ROUTER_DIRECTIVES]
})
export class MenuSuperiorComponent {

    public logout() {
        localStorage.removeItem("TokenManager.token");
        window.location.href = "http://localhost:5001/auth/bodegas/logout";
    }

}