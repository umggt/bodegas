import { Component, OnInit } from "@angular/core"
import { ROUTER_DIRECTIVES } from "@angular/router-deprecated"
import { UnidadesDeMedidaServicio } from "./unidades-de-medida.servicio"
import { OrdenarServicio} from "../ordenar.servicio"
import { UnidadDeMedida } from "./unidades-de-medida.modelos"
import { PaginacionResultado, Dictionary } from "../modelos"
import { PaginaComponent } from "../pagina.component"
import { PaginacionComponent } from "../paginacion.component"

@Component({
    selector: 'unidades-de-medida-listado',
    templateUrl: 'app/mantenimientos/unidades-de-medida-listado.template.html',
    providers: [UnidadesDeMedidaServicio],
    directives: [ROUTER_DIRECTIVES, PaginaComponent, PaginacionComponent]
})
export class UnidadesDeMedidaListadoComponent implements OnInit {
    private pagina: number = null;
    unidadesMedida: PaginacionResultado<UnidadDeMedida>;
    ordenar: OrdenarServicio;

    constructor(private unidadesDeMedidaServicio: UnidadesDeMedidaServicio) {
        this.unidadesMedida = {};
        this.ordenar = new OrdenarServicio();
        this.ordenar.alCambiarOrden.subscribe(this.cambiarOrden);
    }

    ngOnInit() {
        this.obtenerUnidadesDeMedida();
    }

    obtenerUnidadesDeMedida(pagina?: number, campo?: string) {
        var ordenamiento = this.ordenar.campos;
        this.pagina = pagina;
        this.unidadesDeMedidaServicio.obtenerTodos({ pagina: pagina, ordenamiento: ordenamiento }).subscribe(x => {
            this.unidadesMedida = x;
        })
    }
    cambiarPagina(pagina: number) {
        this.obtenerUnidadesDeMedida(pagina);
    }

    cambiarOrden = (columna: string) => {
        this.obtenerUnidadesDeMedida(this.pagina);
    }

    public eliminarUnidadDeMedida(id: number) {
        if (!confirm("Esta seguro de eliminar esta unidad de medida?"))
            return;

        this.unidadesDeMedidaServicio.eliminarUnidadDeMedida(id).subscribe(x => {
            for (var item of this.unidadesMedida.elementos) {
                if (item.id == id)
                    this.unidadesMedida.elementos.splice(this.unidadesMedida.elementos.indexOf(item), 1);
            }
        });
    }

}