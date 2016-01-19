(function () {
    'use strict';

    angular
        .module('app')
        .controller('LiveActivityController', LiveActivityController);

    LiveActivityController.$inject = ['$scope', '$timeout', 'Restangular', '$interval', '_'];

    function LiveActivityController($scope, $timeout, Restangular, $interval, _) {
        var vm = this;

        vm.data;
        vm.requestDate;
        vm.watch = watch;

        vm.get = get;

        activate();

        function activate() {
            get();
        }

        function get() {
            Restangular
                .one('region', vm.region)
            .all('leaderboard')
            .all(vm.bracket)
            .getList({ 'region': vm.region })
            .then(function (data) {
                vm.data = data;
                $scope.$emit('PlayersDetected');
            });
        }

        function watch(player) {
            $scope.$emit('WatchPlayer', {
                name: player.name,
                realm: player.realm.name,
                region: player.regionID,
                playerClass: player.detectedClass,
                specialization: player.detectedSpec,
                gender: player.detectedGenderID,
                race: player.detectedRace,
                faction: player.detectedFaction
            });
        }

    }
})();
