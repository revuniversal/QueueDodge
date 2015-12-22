(function () {
    'use strict';

    angular
        .module('app')
        .controller('ActivityWatcherController', ActivityWatcherController);

    ActivityWatcherController.$inject = ['$timeout', '_', 'Restangular'];

    function ActivityWatcherController($timeout, _, Restangular) {
        var vm = this;

        vm.player = {};
        vm.players = [];
        vm.realms = [];

        vm.checkWatch = checkWatch;
        vm.watchPlayer = watchPlayer;
        vm.removeWatch = removeWatch;

        vm.getRealms = getRealms;

        activate();

        function activate() {
            vm.player.region = 0; // us
            getRealms();
        }

        function watchPlayer(data) {

            var player = {};

            if (data) {
                player.name = data.name;
                player.realm = data.realm;
                player.region = data.region;

                player.playerClass = data.playerClass;
                player.specialization = data.specialization;
                player.gender = data.gender;
                player.race = data.race;
                player.faction = data.faction;

                player.ratingProgression = 0,
                player.rankingProgression = 0,
                player.timesSeen = 0

                vm.players.push(player);
            }
            else {
                getCharacter(vm.player.region, vm.player.realm, vm.player.name);
            }


        }

        function checkWatch() {
            $timeout(function () {
                var detected = [];

                _.each(vm.watch, function (item) {

                    _.each(vm.players, function (player) {
                        var spotted = (item.name == player.name && item.realm.name == player.realm);

                        if (spotted) {
                            player.timesSeen++;
                            player.ratingProgression = player.ratingProgression + (item.detectedRating - item.previousRating);
                            player.rankingProgression = player.rankingProgression + (item.detectedRanking - item.previousRanking);

                            detected.push(player);
                        }
                    });

                });

                if (detected.length > 0) {
                    var audio = new Audio('app/sounds/alarm.wav');
                    audio.volume = .1;
                    audio.play();
                }
            });
        }

        function removeWatch(player) {
            var item = _.remove(vm.players, player);
        }
        function getRealms() {
            Restangular
                .one('region', vm.player.region)
                .all('realm')
                .getList()
                .then(function (realms) {
                    vm.realms = realms;
                });
        }
        function getCharacter(regionID, realmID, name) {
            var player = {};

            Restangular
            .one('region', regionID)
            .one('realm', realmID)
            .one('character', name)
            .get()
            .then(function (playerCharacter) {
                player.name = name;
                player.realm = realmID;
                player.region = regionID;

                player.playerClass = playerCharacter.classID;
                player.specialization = playerCharacter.specID;
                player.gender = playerCharacter.gender;
                player.race = playerCharacter.raceID;
                player.faction = playerCharacter.factionID;
                player.ratingProgression = 0,
                player.rankingProgression = 0,
                player.timesSeen = 0

                vm.players.push(player);
            });
        }
    }
})();
