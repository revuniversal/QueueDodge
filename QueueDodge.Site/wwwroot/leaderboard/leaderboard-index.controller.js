(function () {
    'use strict';

    angular
        .module('app')
        .controller('LeaderboardIndexController', LeaderboardIndexController);

    LeaderboardIndexController.$inject = ['$scope', 'Restangular', '_'];

    function LeaderboardIndexController($scope, Restangular, _) {
        var vm = this;

        vm.leaderboard;
        vm.numPages = 10;
        vm.filter = {
            bracket: "3v3",
            region: 0, // us
            classes: [],
            specs: [],
            realm: 0,
            itemsPerPage: 50,
            page: 1
        };

        vm.loaded;
        vm.classes;
        vm.realms;

        vm.getLeaderboard = getLeaderboard;
        vm.toggleSpecializations = toggleSpecializations;
        vm.search = search;
        vm.getToken = getToken;
        activate();

        function activate() {
            getLeaderboard();
            getClasses();
            getRealms();
        }

        function getLeaderboard() {
            vm.loaded = false;
            Restangular
                 .all("leaderboard")
                 .customGET("", vm.filter)
                 .then(function (leaderboard) {
                     vm.leaderboard = leaderboard;
                     vm.loaded = true;
                 });
        }

        function search() {
            vm.loaded = false;

            vm.filter.classes = _(vm.classes)
                .where('selected')
                .pluck('id')
                .value();

            vm.filter.specs = _(vm.classes)
                .pluck('specializations')
                .flatten()
                .where('selected')
                .pluck('id')
                .value();

            Restangular
                 .all("leaderboard")
                 .customGET("", vm.filter)
                 .then(function (leaderboard) {
                     vm.leaderboard = leaderboard;
                     vm.loaded = true;
                 });
        }
        function getClasses() {
            Restangular
                .all("classes")
                .getList()
                .then(function (classes) {
                    vm.classes = classes;
                });
        }
        function getRealms() {
            Restangular
                .one('region', vm.filter.region)
                .all('realm')
                .getList()
                .then(function (realms) {
                    vm.realms = realms;
                });
        }

        function toggleSpecializations(c) {
            c.selected = !c.selected;

            for (var x = 0; x < c.specializations.length; x++) {
                c.specializations[x].selected = c.selected;
            }
        }
        
        function getToken(){
            var code = window.location.search.replace("?code=","").replace("&state=","");

            Restangular
            .all("battlenet")
            .one("oauth", code)
            .get()
            .then(function(token){
            alert(token);
            });
        }
    }
})();
