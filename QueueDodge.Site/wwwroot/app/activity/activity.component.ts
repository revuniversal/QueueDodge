import {Component, OnInit} from 'angular2/core';
import {RouteParams, Router} from 'angular2/router';
import {LiveComponent} from './live/live.component';
import {WatcherComponent} from './watcher/watcher.component';
import {RegionService} from '../services/region.service';

@Component({
    selector: 'activity',
    templateUrl: '../app/activity/activity.component.html',
    directives: [LiveComponent, WatcherComponent]
})
export class ActivityComponent {
    private router: Router;

    public region: string;
    public bracket: string;

    constructor(regionService: RegionService, routeParams: RouteParams, router:Router) {
        this.region = routeParams.get("region");
        this.bracket = routeParams.get("bracket");
        regionService.regionChanged.subscribe(region => this.regionChanged(region));
        this.router = router;
    }

    public regionChanged(region: string): void {
        this.router.navigate(['Activity', { region: region, bracket: this.bracket}]);
    }
}