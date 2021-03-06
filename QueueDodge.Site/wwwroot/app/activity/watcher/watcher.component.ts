﻿import {Component, Input, OnInit} from "@angular/core";
import {CORE_DIRECTIVES} from "@angular/common";

import {WatcherService} from "./watcher.service";
import {WatchedPlayer} from "./watched-player";
import {ActivityService} from "../activity.service";
import {CharacterLookupComponent} from './character-lookup/character-lookup.component';

@Component({
    selector: "watcher",
    templateUrl: "../app/activity/watcher/watcher.component.html",
    directives: [CORE_DIRECTIVES,CharacterLookupComponent]
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