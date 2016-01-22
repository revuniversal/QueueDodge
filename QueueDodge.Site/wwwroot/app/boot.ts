import {bootstrap}    from 'angular2/platform/browser';
import {QueueDodgeComponent} from './queue-dodge.component';
import {ROUTER_PROVIDERS} from 'angular2/router';

import {RegionService} from './services/region.service';
import {RealmService} from './services/realm.service';
import {CharacterService} from './services/character.service';
import {ClassService} from './services/class.service';
import {LeaderboardService} from './services/leaderboard.service';
import {ActivityService} from './services/activity.service';

import {Http, HTTP_PROVIDERS} from 'angular2/http';

bootstrap(QueueDodgeComponent, [
    ROUTER_PROVIDERS,
    HTTP_PROVIDERS,
    RegionService
]);