System.register(['angular2/core', './WatchedPlayer'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, WatchedPlayer_1;
    var WatcherService;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (WatchedPlayer_1_1) {
                WatchedPlayer_1 = WatchedPlayer_1_1;
            }],
        execute: function() {
            WatcherService = (function () {
                function WatcherService() {
                    this.watchedPlayers = [];
                }
                WatcherService.prototype.watch = function (player) {
                    var watchedPlayer = this.convert(player);
                    this.watchedPlayers.push(watchedPlayer);
                };
                WatcherService.prototype.ignore = function (player) {
                    var watchedPlayer = this.findPlayerByWatch(player);
                    this.watchedPlayers.splice(watchedPlayer, 1);
                };
                WatcherService.prototype.detected = function (player) {
                    var watchedPlayer = this.findPlayerByLadderChange(player);
                    if (watchedPlayer != undefined) {
                        console.log(watchedPlayer.name + " spotted!");
                        // TODO:  Play sound here.
                        var audio = new Audio('chime.wav');
                        audio.play();
                        watchedPlayer.rankingProgress += (player.previous.ranking - player.current.ranking);
                        watchedPlayer.ratingProgress += (player.current.rating - player.previous.rating);
                        watchedPlayer.timesSeen += 1;
                    }
                };
                WatcherService.prototype.playerIsWatched = function (player) {
                    var foundPlayer = this.findPlayerByLadderChange(player);
                    return foundPlayer != null;
                };
                WatcherService.prototype.findPlayerByLadderChange = function (player) {
                    for (var x = 0; x < this.watchedPlayers.length; x++) {
                        var p = this.watchedPlayers[x];
                        if (p.name === player.current.character.name && p.realm === player.current.character.realm.name && p.regionID === player.current.character.realm.region) {
                            return p;
                        }
                    }
                };
                WatcherService.prototype.findPlayerByWatch = function (player) {
                    for (var x = 0; x < this.watchedPlayers.length; x++) {
                        var p = this.watchedPlayers[x];
                        if (p.name === player.name && p.realm === player.realm && p.regionID === player.regionID) {
                            return x;
                        }
                    }
                };
                WatcherService.prototype.convert = function (player) {
                    var watchedPlayer = new WatchedPlayer_1.WatchedPlayer();
                    watchedPlayer.name = player.current.character.name;
                    watchedPlayer.realm = player.current.character.realm.name;
                    watchedPlayer.regionID = player.current.character.realm.region;
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
                };
                WatcherService = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [])
                ], WatcherService);
                return WatcherService;
            })();
            exports_1("WatcherService", WatcherService);
        }
    }
});
//# sourceMappingURL=watcher.service.js.map