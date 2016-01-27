import {Realm} from './Realm';

export class LadderChange {
    public id: number;

    public bracket: string;
    public name: string;

    public previousFaction: number;
    public detectedFaction: number;
    public previousRace: number;
    public detectedRace: number;
    public previousClass: number;
    public detectedClass: number;
    public previousSpec: number;
    public detectedSpec: number;
    public previousRanking: number;
    public previousRating: number;
    public detectedRanking: number;
    public detectedRating: number;
    public previousSeasonWins: number;
    public detectedSeasonWins: number;
    public previousSeasonLosses: number;
    public detectedSeasonLosses: number;
    public previousWeeklyLosses: number;
    public detectedWeeklyLosses: number;
    public previousWeeklyWins: number;
    public detectedWeeklyWins: number;
    public previousGenderID: number;
    public detectedGenderID: number;

    public realm: Realm;

}