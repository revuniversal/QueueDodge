import {Component} from 'angular2/core';
import {CORE_DIRECTIVES} from 'angular2/common';
import {RegionService} from '../services/region.service';

@Component({
    selector: 'home',
    templateUrl: '../../app/home/home.component.html',
    providers: [RegionService],
    directives: [CORE_DIRECTIVES]
})
export class HomeComponent{
}