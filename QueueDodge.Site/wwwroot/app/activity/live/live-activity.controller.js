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

            //var connection = $.hubConnection();
            //var leaderboardHubProxy = connection.createHubProxy('leaderboardHub');
            //var requestStartedEventName = 'request_' + vm.region + '_' + vm.bracket;
            //var requestCompleteEventName = 'refresh_' + vm.region + '_' + vm.bracket;

            //leaderboardHubProxy.on(requestCompleteEventName, function (activity, completedTime) {
            //    vm.data = activity;
            //    vm.requestDate = completedTime;
            //    $scope.$emit('PlayersDetected');
            //    $scope.$apply();
            //});

            //connection.start()
            //    .done(function () { console.log('Now connected, connection ID=' + connection.id); })
            //    .fail(function () { console.log('Could not connect'); });

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
