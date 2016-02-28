import {Component, Input, OnInit} from 'angular2/core';
import {CORE_DIRECTIVES} from 'angular2/common';
import {WatcherService} from './watcher.service';
import {WatchedPlayer} from './WatchedPlayer';
import {ActivityService} from '../activity.service';

@Component({
    selector: 'watcher',
    templateUrl: '../app/activity/watcher/watcher.component.html',
    directives: [CORE_DIRECTIVES]
})
export class WatcherComponent implements OnInit {
    @Input() region: string;
    @Input() bracket: string;

    private watcher: WatcherService;

    public players: Array<WatchedPlayer>;
    public activityService: ActivityService;

    constructor(watcher: WatcherService, activityService: ActivityService) {
        this.watcher = watcher;
        this.activityService = activityService;
    }

    public ngOnInit(): void {
        let players: Array<WatchedPlayer> = this.watcher.getFromLocalStorage(this.region, this.bracket);

        this.players = players;
        this.watcher.watchedPlayers = players;

    }

    public ignore(player: WatchedPlayer): void {
        this.watcher.ignore(player, this.region, this.bracket);
    }
}