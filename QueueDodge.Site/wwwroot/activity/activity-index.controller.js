(function () {
    'use strict';

    angular
        .module('app')
        .controller('ActivityIndexController', ActivityIndexController);

    ActivityIndexController.$inject = ['$scope', '$stateParams', 'Restangular', '$interval', '_'];

    function ActivityIndexController($scope, $stateParams, Restangular, $interval, _) {
        var vm = this;
        vm.bracket = $stateParams.bracket;
        vm.region = 'us';

        vm.detectedPlayers;


        $scope.$on('PlayersDetected', function () {
            $scope.$broadcast('CheckWatchList');
        });

        $scope.$on('WatchPlayer', function (event, data) {
            $scope.$broadcast('Watch', data);
        });
    }
})();
