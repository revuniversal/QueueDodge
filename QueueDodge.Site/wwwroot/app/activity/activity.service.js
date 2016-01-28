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
    var ActivityService;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            }],
        execute: function() {
            ActivityService = (function () {
                function ActivityService() {
                }
                ActivityService.prototype.positive = function (number) {
                    return number > 0;
                };
                ActivityService.prototype.ratingIncrease = function (previousRating, detectedRating) {
                    return detectedRating > previousRating;
                };
                ActivityService.prototype.rankingIncrease = function (previousRanking, detectedRanking) {
                    return detectedRanking < previousRanking;
                };
                ActivityService.prototype.isAlliance = function (faction) {
                    return faction === 0;
                };
                ActivityService.prototype.isHorde = function (faction) {
                    return faction === 1;
                };
                ActivityService = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [])
                ], ActivityService);
                return ActivityService;
            })();
            exports_1("ActivityService", ActivityService);
        }
    }
});
//# sourceMappingURL=activity.service.js.map