/// <reference path="../../node_modules/angular2/typings/browser.d.ts" />
import {bootstrap} from "angular2/platform/browser"
import {MainComponent} from "./main.component"

import "ts-helpers";
import 'rxjs/add/operator/map';

var manager = bodega.tokenManager;
if (manager.access_token && manager.id_token && manager.profile && !manager.expired) {
    bootstrap(MainComponent);
}
