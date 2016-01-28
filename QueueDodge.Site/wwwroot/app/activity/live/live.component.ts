﻿import {Component, Input, OnInit, OnDestroy} from 'angular2/core';
import {CORE_DIRECTIVES} from 'angular2/common';
import {LiveService} from './live.service';
import {WatcherService} from '../watcher/watcher.service';
import {LadderChange} from '../../models/LadderChange';
import {ActivityService} from '../activity.service';
import {WatchedPlayer} from '../watcher/WatchedPlayer';
@Component({
    selector: 'live',
    templateUrl: '../app/activity/live/live.component.html',
    directives: [CORE_DIRECTIVES]
})
export class LiveComponent implements OnInit, OnDestroy {
    @Input() region: string;
    @Input() bracket: string;

    public liveService: LiveService;
    private watcher: WatcherService;
    public activity: Array<LadderChange>;
    public activityService: ActivityService;

    constructor(activityService: ActivityService, liveService: LiveService, watcher: WatcherService) {
        this.activity = [];
        this.liveService = liveService;
        this.watcher = watcher;
        this.activityService = activityService;
    }

    public ngOnInit(): void {
        this.liveService.connect(this.bracket, this.region);
        this.liveService.activityDetected.subscribe((activity: any) => this.addActivity(activity));
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
            this.watcher.detected(activity);

            if (!this.watcher.playerIsWatched(activity)) {
                console.log("this player is watched, not displaying in live component");
            } else {
                this.activity.push(activity);
            }
        }
    }
    public watch(player: LadderChange) {
        this.watcher.watch(player);
        let index: number = this.activity.indexOf(player);
        this.activity.splice(index,1);
    }
}