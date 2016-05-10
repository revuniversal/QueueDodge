﻿import {Injectable, EventEmitter} from '@angular/core';
import {Http, Response, HTTP_PROVIDERS} from '@angular/http';

@Injectable()
export class ActivityService {

    public positive(number: number): boolean {
        return number > 0;
    }
    public ratingIncrease(previousRating: number, detectedRating: number): boolean {
        return detectedRating > previousRating;
    }
    public rankingIncrease(previousRanking: number, detectedRanking: number): boolean {
        return detectedRanking < previousRanking;
    }
    public isAlliance(faction: number): boolean {
        return faction === 1
    }
    public isHorde(faction: number): boolean {
        return faction === 2
    }
}