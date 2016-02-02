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

            watchedPlayer.rankingProgress += (player.previous.ranking - player.current.ranking)
            watchedPlayer.ratingProgress += (player.current.rating - player.previous.rating);
            watchedPlayer.timesSeen += 1;
        }
    }

    public playerIsWatched(player: LadderChange): boolean {
        let foundPlayer: WatchedPlayer = this.findPlayerByLadderChange(player);
        return foundPlayer != null;
    }

    private findPlayerByLadderChange(player: LadderChange): WatchedPlayer {
        for (let x = 0; x < this.watchedPlayers.length; x++){
            let p = this.watchedPlayers[x];
            if (p.name === player.current.character.name && p.realm === player.current.character.realm.name && p.regionID === player.current.character.realm.region.id){
                return p;
            }
        }
    }

    private findPlayerByWatch(player: WatchedPlayer): number {
        for (let x = 0; x < this.watchedPlayers.length; x++) {
            let p = this.watchedPlayers[x];
            if (p.name === player.name && p.realm === player.realm && p.regionID === player.regionID) {
                return x;
            }
        }
    }

    private convert(player: LadderChange): WatchedPlayer {
        let watchedPlayer = new WatchedPlayer();

        watchedPlayer.name = player.current.character.name;
        watchedPlayer.realm = player.current.character.realm.name;
        watchedPlayer.regionID = player.current.character.realm.region.id;
        watchedPlayer.raceID = player.current.character.race.id;
        watchedPlayer.factionID = player.current.character.race.faction.id  ;
        watchedPlayer.classID = player.current.character.class.id;
        watchedPlayer.specializationID = player.current.character.specialization.id;
        watchedPlayer.genderID = player.current.character.gender;
        watchedPlayer.rankingProgress = player.current.ranking - player.previous.ranking;
        watchedPlayer.ratingProgress = player.current.rating - player.previous.rating;
        watchedPlayer.timesSeen = 1;

        return watchedPlayer;
    }
}