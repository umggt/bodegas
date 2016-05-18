import { Component, EventEmitter, Input, Output } from "@angular/core"
import { PaginacionBase } from "./modelos"

@Component({
    selector: 'paginacion',
    templateUrl: 'app/paginacion.template.html'
})
export class PaginacionComponent {

    @Input()
    resultado: PaginacionBase;

    @Output()
    alCambiarPagina = new EventEmitter<number>();

    constructor() {
        this.resultado = { paginas: [] };
    }

    paginaAnterior() {
        var pagina = this.resultado.pagina;
        this.cambiarPagina(pagina - 1);
    }

    paginaSiguiente() {
        var pagina = this.resultado.pagina;
        this.cambiarPagina(pagina + 1);
    }

    cambiarPagina(pagina: number) {

        let paginas = this.resultado.paginas;

        if (pagina === -1) {
            let index = paginas.indexOf(pagina);
            if (index === 0) {
                index++;
            } else {
                index--;
            }
            pagina = paginas[index];
        }

        this.alCambiarPagina.emit(pagina);
    }
}