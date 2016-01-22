System.register(['angular2/core', 'angular2/common', '../../services/activity.service'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, common_1, activity_service_1;
    var LiveComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (common_1_1) {
                common_1 = common_1_1;
            },
            function (activity_service_1_1) {
                activity_service_1 = activity_service_1_1;
            }],
        execute: function() {
            LiveComponent = (function () {
                function LiveComponent(activityService) {
                    this.activity = [];
                    this.activityService = activityService;
                }
                LiveComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    this.activityService.connect(this.bracket, this.region);
                    this.activityService.activityDetected.subscribe(function (activity) { return _this.addActivity(activity); });
                };
                LiveComponent.prototype.ngOnDestroy = function () {
                    this.activityService.activityDetected.unsubscribe();
                    this.activityService.disconnect();
                };
                LiveComponent.prototype.addActivity = function (activity) {
                    if (this.activity.length === 50) {
                        this.activity.shift();
                    }
                    this.activity.push(activity);
                };
                LiveComponent.prototype.ratingIncrease = function (previousRating, detectedRating) {
                    return detectedRating > previousRating;
                };
                LiveComponent.prototype.rankingIncrease = function (previousRanking, detectedRanking) {
                    return detectedRanking < previousRanking;
                };
                LiveComponent.prototype.isAlliance = function (faction) {
                    return faction === 0;
                };
                LiveComponent.prototype.isHorde = function (faction) {
                    return faction === 1;
                };
                __decorate([
                    core_1.Input(), 
                    __metadata('design:type', String)
                ], LiveComponent.prototype, "region", void 0);
                __decorate([
                    core_1.Input(), 
                    __metadata('design:type', String)
                ], LiveComponent.prototype, "bracket", void 0);
                LiveComponent = __decorate([
                    core_1.Component({
                        selector: 'live',
                        templateUrl: '../app/activity/live/live.component.html',
                        directives: [common_1.CORE_DIRECTIVES],
                        providers: [activity_service_1.ActivityService]
                    }), 
                    __metadata('design:paramtypes', [activity_service_1.ActivityService])
                ], LiveComponent);
                return LiveComponent;
            })();
            exports_1("LiveComponent", LiveComponent);
        }
    }
});
//# sourceMappingURL=live.component.js.map