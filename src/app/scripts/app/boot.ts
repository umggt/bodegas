/// <reference path="../typings/browser/ambient/es6-shim/index.d.ts" />
import {bootstrap}    from '@angular/platform-browser-dynamic';
import {MainComponent} from "./main.component"

//import "ts-helpers";
import "rxjs/add/operator/map";

var manager = Bodega.tokenManager;
if (manager.access_token && manager.id_token && manager.profile && !manager.expired) {
    bootstrap(MainComponent);
}
