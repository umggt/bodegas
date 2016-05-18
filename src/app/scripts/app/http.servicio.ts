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

                if (object[key] === undefined || object[key] === null) {
                    continue;
                }

                params.append(key, object[key]);
                paramsLength++;
            }

            if (!paramsLength) {
                params = null;
            }
        }

        return params;
    }

}