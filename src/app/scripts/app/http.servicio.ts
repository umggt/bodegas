import { Observable } from 'rxjs/Observable'
import { Http, Headers, RequestMethod, Request, Response, URLSearchParams, RequestOptionsArgs } from "@angular/http"
import { Injectable } from "@angular/core"

@Injectable()
export class HttpServicio {

    constructor(private http: Http) {
    }

    private getDefaultOptions(options: RequestOptionsArgs) {
        if (!options) {
            options = {};
        }

        if (!options.headers) {
            const headers = new Headers();
            headers.append("Content-Type", "application/json");
            headers.append("Authorization", `Bearer ${Bodega.tokenManager.access_token}`);
            options.headers = headers;
        }

        return options;
    }

    get(url: string, options?: RequestOptionsArgs): Observable<Response> {
        var defaultOptions = this.getDefaultOptions(options);
        return this.http.get(url, defaultOptions);
    }

    request(url: string | Request, options?: RequestOptionsArgs): Observable<Response> {
        var defaultOptions = this.getDefaultOptions(options);
        return this.http.request(url, defaultOptions);
    }

    params(object: Object) : URLSearchParams {
        let params: URLSearchParams = null;

        if (object) {
            let paramsLength = 0;
            params = new URLSearchParams();

            for (var key in object) {
                if (!object.hasOwnProperty(key)) {
                    continue;
                }

                var obj = object[key];

                if (obj === undefined || obj === null) {
                    continue;
                }


                if (key === 'ordenamiento' && typeof obj === "object") {

                    let fields = "";
                    let firstField = true;
                    for (var subKey in obj) {

                        if (!obj.hasOwnProperty(subKey)) {
                            continue;
                        }

                        let value = obj[subKey];

                        if (value === undefined) {
                            continue;
                        }

                        let prefix = firstField ? "" : ",";

                        let order = ""; //asc
                        if (obj[subKey] === false) {
                            order = "-"; //desc
                        }

                        fields += `${prefix}${order}${subKey}`;
                        firstField = false;
                    }

                    if (fields.length > 0) {
                        params.append(key, fields);
                    }
                    

                } else {
                    params.append(key, obj);
                }
                paramsLength++;
            }

            if (!paramsLength) {
                params = null;
            }
        }

        return params;
    }

}