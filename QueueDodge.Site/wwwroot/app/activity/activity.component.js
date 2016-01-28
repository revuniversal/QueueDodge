System.register(['angular2/core', 'angular2/router', './live/live.component', './watcher/watcher.component', '../services/region.service', './watcher/watcher.service', './live/live.service', './activity.service'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, router_1, live_component_1, watcher_component_1, region_service_1, watcher_service_1, live_service_1, activity_service_1;
    var ActivityComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (live_component_1_1) {
                live_component_1 = live_component_1_1;
            },
            function (watcher_component_1_1) {
                watcher_component_1 = watcher_component_1_1;
            },
            function (region_service_1_1) {
                region_service_1 = region_service_1_1;
            },
            function (watcher_service_1_1) {
                watcher_service_1 = watcher_service_1_1;
            },
            function (live_service_1_1) {
                live_service_1 = live_service_1_1;
            },
            function (activity_service_1_1) {
                activity_service_1 = activity_service_1_1;
            }],
        execute: function() {
            ActivityComponent = (function () {
                function ActivityComponent(regionService, activityService, watcherService, liveService, routeParams, router) {
                    this.region = routeParams.get("region");
                    this.bracket = routeParams.get("bracket");
                    this.router = router;
                    this.watcherService = watcherService;
                    this.activityService = activityService;
                    this.regionService = regionService;
                    this.liveService = liveService;
                }
                ActivityComponent.prototype.ngOnInit = function () {
                    var _this = this;
                    this.subscription = this.regionService.regionChanged.subscribe(function (region) { return _this.regionChanged(region); });
                };
                ActivityComponent.prototype.ngOnDestroy = function () {
                    this.subscription.unsubscribe();
                };
                ActivityComponent.prototype.regionChanged = function (region) {
                    var hostComponent = this.router.hostComponent.name;
                    alert(hostComponent);
                    this.router.navigate(['Activity', { region: region, bracket: this.bracket }]);
                };
                ActivityComponent = __decorate([
                    core_1.Component({
                        selector: 'activity',
                        templateUrl: '../app/activity/activity.component.html',
                        directives: [live_component_1.LiveComponent, watcher_component_1.WatcherComponent],
                        providers: [activity_service_1.ActivityService, watcher_service_1.WatcherService, live_service_1.LiveService]
                    }), 
                    __metadata('design:paramtypes', [region_service_1.RegionService, activity_service_1.ActivityService, watcher_service_1.WatcherService, live_service_1.LiveService, router_1.RouteParams, router_1.Router])
                ], ActivityComponent);
                return ActivityComponent;
            })();
            exports_1("ActivityComponent", ActivityComponent);
        }
    }
});
//# sourceMappingURL=activity.component.js.map