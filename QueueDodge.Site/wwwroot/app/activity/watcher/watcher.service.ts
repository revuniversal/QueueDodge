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

    public watch(player: LadderChange) {
        let watchedPlayer = this.convert(player);
        this.watchedPlayers.push(watchedPlayer);
    }

    //public ignore(player: WatchedPlayer) {
    //    this.playerIgnored.emit(player);
    //}

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