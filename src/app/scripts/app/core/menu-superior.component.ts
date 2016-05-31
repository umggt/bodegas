import { Component } from "@angular/core"

@Component({
    selector: 'menu-superior',
    templateUrl: 'app/core/menu-superior.template.html'
})
export class MenuSuperiorComponent {

    public logout() {
        localStorage.removeItem("TokenManager.token");
        window.location.href = "http://localhost:5001/auth/bodegas/logout";
    }

    public perfil() {
        window.location.href = "http://localhost:5000/perfil";
    }
}