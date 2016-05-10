import {Component} from '@angular/core';
import {ROUTER_DIRECTIVES, RouteConfig} from '@angular/router-deprecated';
import 'rxjs/Rx';

import {HomeComponent} from './index';
import    {ActivityComponent}from './index';
import    {RegionService} from './index';


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