import { Component, Input } from "@angular/core"

@Component({
    selector: 'pagina',
    templateUrl: 'app/pagina.template.html'
})
export class PaginaComponent {

    @Input()
    titulo: string;
}