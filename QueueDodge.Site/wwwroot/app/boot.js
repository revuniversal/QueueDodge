System.register(['angular2/platform/browser', './queue-dodge.component', 'angular2/router', './services/region.service', 'angular2/http'], function(exports_1) {
    var browser_1, queue_dodge_component_1, router_1, region_service_1, http_1;
    return {
        setters:[
            function (browser_1_1) {
                browser_1 = browser_1_1;
            },
            function (queue_dodge_component_1_1) {
                queue_dodge_component_1 = queue_dodge_component_1_1;
            },
            function (router_1_1) {
                router_1 = router_1_1;
            },
            function (region_service_1_1) {
                region_service_1 = region_service_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            }],
        execute: function() {
            browser_1.bootstrap(queue_dodge_component_1.QueueDodgeComponent, [
                router_1.ROUTER_PROVIDERS,
                http_1.HTTP_PROVIDERS,
                region_service_1.RegionService
            ]);
        }
    }
});
//# sourceMappingURL=boot.js.map