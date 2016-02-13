import {Injectable, EventEmitter} from 'angular2/core';
import {Http, Response, HTTP_PROVIDERS} from 'angular2/http';

@Injectable()
export class LeaderboardService {

    public positive(number: number) {
        return number > 0;
    }
    public negative(number: number) {
        return number < 0;
    }
    public ratingIncrease(previousRating: number, detectedRating: number): boolean {
        return detectedRating > previousRating;
    }
    public rankingIncrease(previousRanking: number, detectedRanking: number): boolean {
        return detectedRanking < previousRanking;
    }
    public isAlliance(faction: number): boolean {
        return faction === 0
    }
    public isHorde(faction: number): boolean {
        return faction === 1
    }
}