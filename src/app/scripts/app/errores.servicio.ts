import { Response } from "@angular/http"


export class ErroresServicio {

    handleResponse(error: Response) {
        if (error.status === 400) {
            var errores = error.json();
            var erroresResult: string[] = [];
            for (var key in errores) {
                for (var i = 0; i < errores[key].length; i++) {
                    erroresResult.push(errores[key][i]);
                }
            }
            return erroresResult;
        } else if (error.status == 304 /*Not Modified*/) {
            return [];
        }
        return [error.text()];
    }

}