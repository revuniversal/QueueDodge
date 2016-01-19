System.register(['angular2/core', './watcher/watcher.component', './live/live.component'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, watcher_component_1, live_component_1;
    var ActivityComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (watcher_component_1_1) {
                watcher_component_1 = watcher_component_1_1;
            },
            function (live_component_1_1) {
                live_component_1 = live_component_1_1;
            }],
        execute: function() {
            ActivityComponent = (function () {
                function ActivityComponent() {
                }
                ActivityComponent = __decorate([
                    core_1.Component({
                        selector: 'activity',
                        templateUrl: '../app/activity/activity.component.html',
                        directives: [watcher_component_1.WatcherComponent, live_component_1.LiveComponent]
                    }), 
                    __metadata('design:paramtypes', [])
                ], ActivityComponent);
                return ActivityComponent;
            })();
            exports_1("ActivityComponent", ActivityComponent);
        }
    }
});
//# sourceMappingURL=activity.component.js.map