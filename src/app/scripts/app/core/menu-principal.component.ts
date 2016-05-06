import { Component, OnInit } from "angular2/core"
import { ROUTER_DIRECTIVES } from "angular2/router"
import { MenuServicio } from "./menu.servicio"
import { IOpcionDeMenu } from "./modelos"

@Component({
    selector: 'menu-principal',
    templateUrl: 'app/core/menu-principal.template.html',
    providers: [MenuServicio],
    directives: [ROUTER_DIRECTIVES]
})
export class MenuPrincipalComponent implements OnInit {

    opciones: IOpcionDeMenu[];
    
    constructor(private menuServicio: MenuServicio) {
    
    }

    ngOnInit() {
        this.menuServicio.obtenerOpciones().subscribe(x => {
            this.opciones = x;
            console.log(x);
        });
    }

    expandir(opcion: IOpcionDeMenu) {
        this.opciones.forEach(item => {
            item.expandido = false;
        });
        opcion.expandido = true;
    }
}