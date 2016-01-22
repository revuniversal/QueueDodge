import {Component, Input} from 'angular2/core';
import {CORE_DIRECTIVES} from 'angular2/common';

@Component({
    selector: 'watcher',
    templateUrl: '../app/activity/watcher/watcher.component.html',
    directives: [CORE_DIRECTIVES]
})
export class WatcherComponent {
    @Input() region: string;
    @Input() bracket: string;
}