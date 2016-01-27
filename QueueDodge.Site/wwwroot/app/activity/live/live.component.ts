import {Component, Input, OnInit, OnDestroy} from 'angular2/core';
import {CORE_DIRECTIVES} from 'angular2/common';
import {LiveService} from './live.service';
import {WatcherService} from '../watcher/watcher.service';
import {LadderChange} from '../../models/LadderChange';

@Component({
    selector: 'live',
    templateUrl: '../app/activity/live/live.component.html',
    directives: [CORE_DIRECTIVES]
})
export class LiveComponent implements OnInit, OnDestroy{
    @Input() region: string;
    @Input() bracket: string;

    public liveService: LiveService;
    private watcher: WatcherService;
    public activity: Array<LadderChange>;

    constructor(liveService: LiveService, watcher: WatcherService) {
        this.activity = [];
        this.liveService = liveService;
        this.watcher = watcher;
    }

    public ngOnInit(): void {
        this.liveService.connect(this.bracket, this.region);
        this.liveService.activityDetected.subscribe((activity:any) => this.addActivity(activity));
    }    
    public ngOnDestroy(): void {
        this.liveService.activityDetected.unsubscribe();
        this.liveService.disconnect();
    }
    public addActivity(activity: any): void {
        if (activity === "clear") {
            this.activity = [];
        }
        else {
            this.activity.push(activity);
        }
    }
    public watch(player: LadderChange) {
        this.watcher.watch(player);
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