import {Component, Input, OnInit} from 'angular2/core';
import {CORE_DIRECTIVES} from 'angular2/common';
import {WatcherService} from './watcher.service';
import {WatchedPlayer} from './WatchedPlayer';
import 'rxjs/Rx';

@Component({
    selector: 'watcher',
    templateUrl: '../app/activity/watcher/watcher.component.html',
    directives: [CORE_DIRECTIVES]
})
export class WatcherComponent{
    private watcher: WatcherService;
    public players: Array<WatchedPlayer>;

    constructor(watcher:WatcherService) {
        this.watcher = watcher;
    }

}