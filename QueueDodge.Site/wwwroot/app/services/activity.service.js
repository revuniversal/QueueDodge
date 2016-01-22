System.register(['angular2/core', 'angular2/http'], function(exports_1) {
    var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
        var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
        if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
        else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
        return c > 3 && r && Object.defineProperty(target, key, r), r;
    };
    var __metadata = (this && this.__metadata) || function (k, v) {
        if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
    };
    var core_1, http_1;
    var ActivityService;
    return {
        setters:[
            function (core_1_1) {
                core_1 = core_1_1;
            },
            function (http_1_1) {
                http_1 = http_1_1;
            }],
        execute: function() {
            ActivityService = (function () {
                function ActivityService(http) {
                    this.activityDetected = new core_1.EventEmitter();
                    this.http = http;
                }
                ActivityService.prototype.connect = function (bracket, region) {
                    var _this = this;
                    this.socket = new WebSocket("wss://localhost/ws/" + region + "/" + bracket);
                    this.socket.onopen = function (event) { return _this.onConnect(event, region, bracket); };
                    this.socket.onmessage = function (event) { return _this.onMessage(event, _this); };
                    this.socket.onclose = function (event) { return _this.onClose(event, region, bracket); };
                    this.socket.onerror = function (event) { return _this.onError(event, region, bracket); };
                };
                ActivityService.prototype.disconnect = function () {
                    this.socket.close();
                };
                ActivityService.prototype.onConnect = function (ev, region, bracket) {
                    console.log("connected " + region + " " + bracket);
                };
                ActivityService.prototype.onMessage = function (ev, service) {
                    service.activityDetected.emit(JSON.parse(ev.data));
                };
                ActivityService.prototype.onClose = function (ev, region, bracket) {
                    console.log("closed " + region + " " + bracket);
                };
                ActivityService.prototype.onError = function (ev, region, bracket) {
                    console.log("error " + region + " " + bracket);
                };
                ActivityService.prototype.getActivity = function (region, bracket) {
                    return this
                        .http
                        .get("api/leaderboard/activity?region=" + region + "&bracket=" + bracket + "&locale=en_us")
                        .map(function (res) { return res.json(); });
                };
                ActivityService = __decorate([
                    core_1.Injectable(), 
                    __metadata('design:paramtypes', [http_1.Http])
                ], ActivityService);
                return ActivityService;
            })();
            exports_1("ActivityService", ActivityService);
        }
    }
});
//# sourceMappingURL=activity.service.js.map