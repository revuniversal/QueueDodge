import {Injectable, EventEmitter, OnInit} from '@angular/core';

import {WatchedPlayer} from './watched-player';
import {LadderChange} from '../../models/ladder-change';
import {Realm} from '../../models/realm';
import {Region} from '../../models/region';


@Injectable()
export class WatcherService {
    public watchedPlayers: Array<WatchedPlayer>;

    constructor() {
        this.watchedPlayers = [];
    }

    public watch(player: LadderChange): void {
        let watchedPlayer = this.convert(player);
        this.watchedPlayers.push(watchedPlayer);
        this.addToLocalStorage(player);
    }
    
    public ignore(player: WatchedPlayer, region:string, bracket:string): void {
        let watchedPlayer: number = this.findPlayerByWatch(player);
        this.watchedPlayers.splice(watchedPlayer, 1);
        this.removeFromLocalStorage(player, region, bracket);
    }
    
    public detected(player: LadderChange): void {
        let watchedPlayer = this.findPlayerByLadderChange(player);

        if (watchedPlayer != undefined) {
            console.log(watchedPlayer.name + " spotted!");
            let audio = new Audio('chime.wav');
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
        for (let x = 0; x < this.watchedPlayers.length; x++) {
            let p = this.watchedPlayers[x];
            if (p.name === player.current.character.name && p.realm === player.current.character.realm.name && p.regionID === player.current.character.realm.region.id) {
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
        watchedPlayer.factionID = player.current.character.race.faction.id;
        watchedPlayer.classID = player.current.character.class.id;
        watchedPlayer.specializationID = player.current.character.specialization.id;
        watchedPlayer.genderID = player.current.character.gender;
        watchedPlayer.ranking = player.current.ranking;
        watchedPlayer.rankingProgress = player.current.ranking - player.previous.ranking;
        watchedPlayer.rating = player.current.rating;
        watchedPlayer.ratingProgress = player.current.rating - player.previous.rating;
        watchedPlayer.timesSeen = 1;

        return watchedPlayer;
    }


    // TODO:  Move this into a repository.  Instantiate at the activity level.
    public addToLocalStorage(player: LadderChange) {
        let region: string = player.current.character.realm.region.name;
        let bracket: string = player.current.bracket;
        let key: string = this.getKey(region, bracket);
        let players: Array<WatchedPlayer> = this.getFromLocalStorage(region, bracket);
        let watchedPlayer: WatchedPlayer = this.convert(player);

        players.push(watchedPlayer);

        let watchedPlayersJson: string = JSON.stringify(players);
        localStorage.setItem(key, watchedPlayersJson);
    }
    public removeFromLocalStorage(player: WatchedPlayer, region:string, bracket:string):void {
        let key: string = this.getKey(region, bracket);
        let players = this.getFromLocalStorage(region, bracket);
        let watchedPlayer: number = this.findPlayerByWatch(player);

        players.splice(watchedPlayer, 1);

        let watchedPlayersJson: string = JSON.stringify(players);
        localStorage.setItem(key, watchedPlayersJson);
    }
    public getFromLocalStorage(region: string, bracket: string): Array<WatchedPlayer> {
        let key: string = this.getKey(region, bracket);
        let json = localStorage.getItem(key);
        let players: any = JSON.parse(json);

        if (players == null) {
            players = [];
        }

        return players;
    }
    private getKey(region: string, bracket: string): string {
        let key: string = 'watched:' + region + ':' + bracket;
        return key;
    }
}