(function () {
    'use strict';

    angular
        .module('app')
        .controller('IndexController', IndexController);

    IndexController.$inject = ['$scope', 'Restangular', '$interval'];

    function IndexController($scope, Restangular, $interval) {
        var vm = this;

        vm.region;


        activate();

        $scope.$on("$destroy", function () {
            if (angular.isDefined($scope.Timer)) {
                $interval.cancel($scope.Timer);
            }
        });
        function activate() {
            vm.region = 'us';

            if (!("WebSocket" in window)) {
                // Tell the user to not use a terrible browser.
                alert("Web sockets not enabled.  Come back when you have a real browser.")
            } else {
                connect();
            }
        }
        function connect() {
            try {
                var host = "ws://localhost:5001/ws";
                var socket = new WebSocket(host);

               alert(socket.readyState);

                socket.onopen = function () {
                    alert(socket.readyState);
                }

                socket.onmessage = function (msg) {
                    alert(socket.readyState);
                }

                socket.onclose = function () {
                    alert(socket.readyState);
                }

            } catch (exception) {
                alert(socket.readyState);
            }
        }
    }
})();
