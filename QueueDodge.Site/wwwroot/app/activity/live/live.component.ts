import {Component, Input, OnInit, OnDestroy} from 'angular2/core';
import {CORE_DIRECTIVES} from 'angular2/common';
import {ActivityService} from '../../services/activity.service';
@Component({
    selector: 'live',
    templateUrl: '../app/activity/live/live.component.html',
    directives: [CORE_DIRECTIVES],
    providers: [ActivityService]
})
export class LiveComponent implements OnInit, OnDestroy{
    @Input() region: string;
    @Input() bracket: string;

    public activityService: ActivityService;
    public activity: Array<any>;

    constructor(activityService: ActivityService) {
        this.activity = [];
        this.activityService = activityService;
    }

    ngOnInit()
    {
        this.activityService.connect(this.bracket, this.region);
        this.activityService.activityDetected.subscribe(activity => this.addActivity(activity));
    }
    
    ngOnDestroy() {
        this.activityService.activityDetected.unsubscribe();
        this.activityService.disconnect();
    }

    public addActivity(activity: any): void {

        if (this.activity.length === 50) {
            this.activity.shift();
        }

        this.activity.push(activity);
    }

    public ratingIncrease(previousRating: number, detectedRating: number): boolean {
        return detectedRating > previousRating;
    }
    public rankingIncrease(previousRanking: number, detectedRanking: number): boolean {
        return detectedRanking < previousRanking;
    }
    public isAlliance(faction: number): boolean {
        return faction === 0
    }
    public isHorde(faction: number): boolean {
        return faction === 1
    }
}