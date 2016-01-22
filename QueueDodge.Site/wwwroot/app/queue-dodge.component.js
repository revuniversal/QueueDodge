System.register(['angular2/core', 'angular2/router', './home/home.component', './activity/activity.component', './services/region.service', 'rxjs/Rx'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, router_1, home_component_1, activity_component_1, region_service_1;
    var QueueDodgeComponent;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (home_component_1_1) {
                home_component_1 = home_component_1_1;
            },
            function (activity_component_1_1) {
                activity_component_1 = activity_component_1_1;
            },
            function (region_service_1_1) {
                region_service_1 = region_service_1_1;
            },
            function (_1) {}],
        execute: function() {
            QueueDodgeComponent = (function () {
                function QueueDodgeComponent(regionService) {
                    this.regionService = regionService;
                    this.region = this.regionService.region;
                }
                QueueDodgeComponent.prototype.changeRegion = function (region) {
                    this.regionService.changeRegion(region);
                    this.region = region;
                };
                QueueDodgeComponent = __decorate([
                    core_1.Component({
                        selector: 'queue-dodge',
                        templateUrl: '../app/queue-dodge.component.html',
                        directives: [router_1.ROUTER_DIRECTIVES]
                    }),
                    router_1.RouteConfig([
                        { path: '/', name: 'Home', component: home_component_1.HomeComponent, useAsDefault: true },
                        { path: '/activity/:region/:bracket', name: 'Activity', component: activity_component_1.ActivityComponent }
                    ]), 
                    __metadata('design:paramtypes', [region_service_1.RegionService])
                ], QueueDodgeComponent);
                return QueueDodgeComponent;
            })();
            exports_1("QueueDodgeComponent", QueueDodgeComponent);
        }
    }
});
//# sourceMappingURL=queue-dodge.component.js.map