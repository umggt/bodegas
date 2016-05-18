/// <reference path="../typings/browser/ambient/es6-shim/index.d.ts" />
import {bootstrap}    from '@angular/platform-browser-dynamic';
import {MainComponent} from "./main.component"
import {HttpServicio} from "./http.servicio"
import {HTTP_PROVIDERS} from "@angular/http"

import "rxjs/add/operator/map";

var manager = Bodega.tokenManager;
if (manager.access_token && manager.id_token && manager.profile && !manager.expired) {
    bootstrap(MainComponent, [HttpServicio, HTTP_PROVIDERS]);
}
