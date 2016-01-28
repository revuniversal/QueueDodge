System.register(['angular2/core', 'angular2/common', './watcher.service', '../activity.service'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, common_1, watcher_service_1, activity_service_1;
    var WatcherComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (common_1_1) {
                common_1 = common_1_1;
            },
            function (watcher_service_1_1) {
                watcher_service_1 = watcher_service_1_1;
            },
            function (activity_service_1_1) {
                activity_service_1 = activity_service_1_1;
            }],
        execute: function() {
            WatcherComponent = (function () {
                function WatcherComponent(watcher, activityService) {
                    this.watcher = watcher;
                    this.activityService = activityService;
                }
                WatcherComponent = __decorate([
                    core_1.Component({
                        selector: 'watcher',
                        templateUrl: '../app/activity/watcher/watcher.component.html',
                        directives: [common_1.CORE_DIRECTIVES]
                    }), 
                    __metadata('design:paramtypes', [watcher_service_1.WatcherService, activity_service_1.ActivityService])
                ], WatcherComponent);
                return WatcherComponent;
            })();
            exports_1("WatcherComponent", WatcherComponent);
        }
    }
});
//# sourceMappingURL=watcher.component.js.map