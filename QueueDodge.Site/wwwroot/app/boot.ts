import {bootstrap}    from 'angular2/platform/browser';
import {QueueDodgeComponent} from './queue-dodge.component';
import {ROUTER_PROVIDERS} from 'angular2/router';
import {RegionService} from './services/region.service';
import {Http, HTTP_PROVIDERS} from 'angular2/http';

bootstrap(QueueDodgeComponent, [
    ROUTER_PROVIDERS,
    HTTP_PROVIDERS,
    RegionService
]);
