/// <reference path="../../../typings/lodash/lodash.d.ts" />
import {Injectable, EventEmitter} from 'angular2/core';
import {WatchedPlayer} from './WatchedPlayer';
import {LadderChange} from '../../models/LadderChange';
import {Realm} from '../../models/Realm';
import {Region} from '../../models/Region';

@Injectable()
export class WatcherService {
    public watchedPlayers: Array<WatchedPlayer>;

    constructor() {
        this.watchedPlayers = [];
    }

    public watch(player: LadderChange): void {
        let watchedPlayer = this.convert(player);
        this.watchedPlayers.push(watchedPlayer);
    }

    public detected(player: LadderChange): void {
        let watchedPlayer = this.findPlayer(player);

        if (watchedPlayer != undefined) {
            console.log(watchedPlayer.name + " spotted!");
            // TODO:  Play sound here.
            watchedPlayer.rankingProgress += (player.previousRanking - player.detectedRanking)
            watchedPlayer.ratingProgress += (player.detectedRating - player.previousRating);
            watchedPlayer.timesSeen += 1;
        }
    }

    public playerIsWatched(player: LadderChange): boolean {
        let foundPlayer: WatchedPlayer = this.findPlayer(player);
        return foundPlayer !== undefined;
    }

    private findPlayer(player: LadderChange): WatchedPlayer {
        let watchedPlayer = _.find(this.watchedPlayers, { 'name': player.name, 'realm': player.realm.name, 'regionID': player.realm.region.id });

        return watchedPlayer;
    }

    private convert(player: LadderChange): WatchedPlayer {
        let watchedPlayer = new WatchedPlayer();

        watchedPlayer.name = player.name;
        watchedPlayer.realm = player.realm.name;
        watchedPlayer.regionID = player.realm.region.id;
        watchedPlayer.raceID = player.detectedRace;
        watchedPlayer.factionID = player.detectedFaction;
        watchedPlayer.classID = player.detectedClass;
        watchedPlayer.specializationID = player.detectedSpec;
        watchedPlayer.genderID = player.detectedGenderID;
        watchedPlayer.rankingProgress = player.detectedRanking - player.previousRanking;
        watchedPlayer.ratingProgress = player.detectedRating - player.previousRating;
        watchedPlayer.timesSeen = 1;

        return watchedPlayer;
    }
}