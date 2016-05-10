import {Component, OnInit, OnDestroy, EventEmitter} from '@angular/core';
import {RouteParams, Router} from '@angular/router-deprecated';

import {
    LiveComponent,
    WatcherComponent,
    RegionService,
    WatcherService,
    LiveService,
    ActivityService,
    LadderChange
} from '../index';

@Component({
    selector: 'activity',
    templateUrl: '../app/activity/activity.component.html',
    directives: [LiveComponent, WatcherComponent],
    providers: [ActivityService, WatcherService, LiveService]
})
export class ActivityComponent implements OnInit, OnDestroy{
    private router: Router;
    private regionService: RegionService;
    private subscription: any;

    public activityService: ActivityService;
    public liveService: LiveService;
    public watcherService: WatcherService;
    public region: string;
    public bracket: string;

    constructor(regionService: RegionService,
        activityService:ActivityService,
        watcherService: WatcherService,
        liveService: LiveService,
        routeParams: RouteParams,
        router: Router) {

        this.region = routeParams.get("region");
        this.bracket = routeParams.get("bracket");
        this.router = router;
        this.watcherService = watcherService;
        this.activityService = activityService;
        this.regionService = regionService;
        this.liveService = liveService;
    }

    ngOnInit() {
        this.subscription = this.regionService.regionChanged.subscribe((region: string) => this.regionChanged(region));
    }
    ngOnDestroy() {
        this.subscription.unsubscribe();
    }

    public regionChanged(region: string): void {
        this.router.navigate(['Activity', { region: region, bracket: this.bracket }]);
    }
} 
