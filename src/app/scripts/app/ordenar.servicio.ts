import { Dictionary } from "./modelos"
import { EventEmitter } from "@angular/core"

export class OrdenarServicio {

    campos: Dictionary<boolean> = {};
    alCambiarOrden: EventEmitter<string>;

    constructor() {
        this.alCambiarOrden = new EventEmitter<string>();
    }

    porCampo(campo: string) {

        if (this.campos[campo]) {
            this.campos[campo] = false;
        } else if (this.campos[campo] === false) {
            this.campos[campo] = undefined;
        } else {
            this.campos[campo] = true;
        }

        this.alCambiarOrden.emit(campo);
    }

    reiniciar() {
        this.campos = {};
    }

    css(campo: string) {
        if (this.campos[campo]) return "asc";
        if (this.campos[campo] === false) return "desc";
        return null;
    }
}