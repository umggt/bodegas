import { Dictionary } from "./modelos"

export class OrdenarServicio {

    campos: Dictionary<boolean> = {};

    porCampo(campo: string) {

        if (this.campos[campo]) {
            this.campos[campo] = false;
        } else if (this.campos[campo] === false) {
            this.campos[campo] = undefined;
        } else {
            this.campos[campo] = true;
        }

        console.log(this.campos);
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