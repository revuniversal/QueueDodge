System.register(['angular2/core'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1;
    var LeaderboardService;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            }],
        execute: function() {
            LeaderboardService = (function () {
                function LeaderboardService() {
                }
                LeaderboardService.prototype.positive = function (number) {
                    return number > 0;
                };
                LeaderboardService.prototype.negative = function (number) {
                    return number < 0;
                };
                LeaderboardService.prototype.ratingIncrease = function (previousRating, detectedRating) {
                    return detectedRating > previousRating;
                };
                LeaderboardService.prototype.rankingIncrease = function (previousRanking, detectedRanking) {
                    return detectedRanking < previousRanking;
                };
                LeaderboardService.prototype.isAlliance = function (faction) {
                    return faction === 0;
                };
                LeaderboardService.prototype.isHorde = function (faction) {
                    return faction === 1;
                };
                LeaderboardService = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [])
                ], LeaderboardService);
                return LeaderboardService;
            })();
            exports_1("LeaderboardService", LeaderboardService);
        }
    }
});
//# sourceMappingURL=leaderboard.service.js.map