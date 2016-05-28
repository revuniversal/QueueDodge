import {bootstrap}    from "@angular/platform-browser-dynamic";
import {ROUTER_PROVIDERS} from "@angular/router-deprecated";
import {Http, HTTP_PROVIDERS} from "@angular/http";

import {QueueDodgeComponent} from "./queue-dodge.component";
import {RegionService} from "./region/region.service";

bootstrap(QueueDodgeComponent, [
    ROUTER_PROVIDERS,
    HTTP_PROVIDERS,
    RegionService
]);
