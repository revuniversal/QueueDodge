import {Component} from 'angular2/core';
import {ROUTER_DIRECTIVES, RouteConfig} from 'angular2/router';
import {HomeComponent} from './home/home.component';
import {ActivityComponent} from './activity/activity.component';
import {RegionService} from './services/region.service';
import 'rxjs/Rx';

@Component({
    selector: 'queue-dodge',
    templateUrl: '../app/queue-dodge.component.html',
    directives: [ROUTER_DIRECTIVES]
})
@RouteConfig([
    { path: '/', name: 'Home', component: HomeComponent, useAsDefault: true },
    { path: '/activity/:region/:bracket', name: 'Activity', component: ActivityComponent }
])
export class QueueDodgeComponent {
    private regionService: RegionService;
    public region:any;

    constructor(regionService: RegionService) {
        this.regionService = regionService;
        this.region = this.regionService.region;
    }

    public changeRegion(region: string) {
        this.regionService.changeRegion(region);
        this.region = region;
    }
}