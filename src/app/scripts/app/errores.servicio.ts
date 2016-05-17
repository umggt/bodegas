import { Response } from "@angular/http"


export class ErroresServicio {

    isNotModifiedResponse(response: Response) {
        return response.status == 304 /* Not Modified */;
    }
    
    obtenerErrores(response: Response) {
        if (response.status === 400 /* Bad request, tal vez error de validación*/) {
            var errores = response.json();
            var erroresResult: string[] = [];
            for (var key in errores) {
                for (var i = 0; i < errores[key].length; i++) {
                    erroresResult.push(errores[key][i]);
                }
            }
            return erroresResult;
        }
        return [response.text()];
    }

}