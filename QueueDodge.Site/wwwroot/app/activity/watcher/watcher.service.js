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
                WatcherService.prototype.detected = function (player) {
                    var watchedPlayer = this.findPlayer(player);
                    if (watchedPlayer != undefined) {
                        console.log(watchedPlayer.name + " spotted!");
                        // TODO:  Play sound here.
                        watchedPlayer.rankingProgress += (player.previousRanking - player.detectedRanking);
                        watchedPlayer.ratingProgress += (player.detectedRating - player.previousRating);
                        watchedPlayer.timesSeen += 1;
                    }
                };
                WatcherService.prototype.playerIsWatched = function (player) {
                    var foundPlayer = this.findPlayer(player);
                    return foundPlayer !== undefined;
                };
                WatcherService.prototype.findPlayer = function (player) {
                    var watchedPlayer = _.find(this.watchedPlayers, { 'name': player.name, 'realm': player.realm.name, 'regionID': player.realm.region.id });
                    return watchedPlayer;
                };
                WatcherService.prototype.convert = function (player) {
                    var watchedPlayer = new WatchedPlayer_1.WatchedPlayer();
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