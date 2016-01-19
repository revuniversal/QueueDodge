import {Component} from 'angular2/core';
import {RouteConfig, ROUTER_DIRECTIVES} from 'angular2/router';
import {WatcherComponent} from './watcher/watcher.component';
import {LiveComponent} from './live/live.component';
@Component({
    selector: 'activity',
    templateUrl: '../app/activity/activity.component.html',
    directives : [WatcherComponent, LiveComponent]
})
export class ActivityComponent {
}