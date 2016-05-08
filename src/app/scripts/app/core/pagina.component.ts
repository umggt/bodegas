import { Component, Input } from "@angular/core"

@Component({
    selector: 'pagina',
    templateUrl: 'app/core/pagina.template.html'
})
export class PaginaComponent {

    @Input()
    titulo: string;
}