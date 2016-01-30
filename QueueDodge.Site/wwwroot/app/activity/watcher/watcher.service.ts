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

    public ignore(player: WatchedPlayer):void {
        let watchedPlayer: number = this.findPlayerByWatch(player);
        this.watchedPlayers.splice(watchedPlayer, 1);
    }
   
    public detected(player: LadderChange): void {
        let watchedPlayer = this.findPlayerByLadderChange(player);

        if (watchedPlayer != undefined) {
            console.log(watchedPlayer.name + " spotted!");
            // TODO:  Play sound here.
            var audio = new Audio('audio_file.mp3');
            audio.play();

            watchedPlayer.rankingProgress += (player.previousRanking - player.detectedRanking)
            watchedPlayer.ratingProgress += (player.detectedRating - player.previousRating);
            watchedPlayer.timesSeen += 1;
        }
    }

    public playerIsWatched(player: LadderChange): boolean {
        let foundPlayer: WatchedPlayer = this.findPlayerByLadderChange(player);
        return foundPlayer != null;
    }

    private findPlayerByLadderChange(player: LadderChange): WatchedPlayer {
        //let watchedPlayer = _.find(this.watchedPlayers, { 'name': player.name, 'realm': player.realm.name, 'regionID': player.realm.region.id });
        for (let x = 0; x < this.watchedPlayers.length; x++){
            let p = this.watchedPlayers[x];
            if (p.name === player.name && p.realm === player.realm.name && p.regionID === player.realm.region.id){
                return p;
            }
        }
    }

    private findPlayerByWatch(player: WatchedPlayer): number {
        //let watchedPlayer = _.find(this.watchedPlayers, { 'name': player.name, 'realm': player.realm.name, 'regionID': player.realm.region.id });
        for (let x = 0; x < this.watchedPlayers.length; x++) {
            let p = this.watchedPlayers[x];
            if (p.name === player.name && p.realm === player.realm && p.regionID === player.regionID) {
                return x;
            }
        }
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