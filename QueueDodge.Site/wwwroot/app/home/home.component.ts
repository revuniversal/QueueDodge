﻿import {Component} from '@angular/core';
import {CORE_DIRECTIVES} from '@angular/common';

import {RegionService} from '../index';

@Component({
    selector: 'home',
    templateUrl: '../../app/home/home.component.html',
    providers: [RegionService],
    directives: [CORE_DIRECTIVES]
})
export class HomeComponent{
}