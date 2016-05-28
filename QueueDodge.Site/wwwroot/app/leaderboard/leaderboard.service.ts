import {Injectable, EventEmitter} from "@angular/core";
import {Http, Response, HTTP_PROVIDERS} from "@angular/http";

@Injectable()
export class LeaderboardService {

    public positive(num: number) {
        return num > 0;
    }
    public negative(num: number) {
        return num < 0;
    }
    public ratingIncrease(previousRating: number, detectedRating: number): boolean {
        return detectedRating > previousRating;
    }
    public rankingIncrease(previousRanking: number, detectedRanking: number): boolean {
        return detectedRanking < previousRanking;
    }
    public isAlliance(faction: number): boolean {
        return faction === 0;
    }
    public isHorde(faction: number): boolean {
        return faction === 1;
    }
}