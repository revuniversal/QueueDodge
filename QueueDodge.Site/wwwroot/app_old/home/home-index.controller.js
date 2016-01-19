(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeIndexController', HomeIndexController);

    HomeIndexController.$inject = ['$location','Restangular'];

    function HomeIndexController($location,Restangular) {
        var vm = this;
        vm.getToken = getToken;
        activate();

        function activate() {
            vm.region = 'us';

            if (!("WebSocket" in window)) {
                // Tell the user to not use a terrible browser.
                alert("Web sockets not enabled.  Come back when you have a real browser.")
            } else {
                connect();
            }
        }

        //function getToken(){
        //    var code = $location.search().code;
        //    Restangular
        //    .all("battlenet")
        //    .one("code", code)
        //    .get()
        //    .then(function(token){
        //    alert(token);
        //    });
        //}

        function getToken() {
            var code = window.location.search.replace("?code=", "").replace("&state=", "");

            Restangular
            .all("battlenet")
            .one("oauth", code)
            .get()
            .then(function (token) {
                alert(token);
            });
        }

        function connect() {
            try {
                var host = "wss://localhost/ws";
                var socket = new WebSocket(host);

                console.log("connecting...");

                socket.onopen = function () {
                    console.log("Connection open.");
                }

                socket.onmessage = function (msg) {
                   // console.log(JSON.stringify(msg.data));
                }

                socket.onclose = function () {
                    console.log("Connection closed.");
                }

             

            } catch (exception) {
                alert(exception)
            }
        }
    }
})();
