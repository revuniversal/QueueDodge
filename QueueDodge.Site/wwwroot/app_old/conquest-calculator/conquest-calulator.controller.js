(function () {
    'use strict';

    angular
        .module('app')
        .controller('ConquestCalculatorController', ConquestCalculatorController);

    ConquestCalculatorController.$inject = ['Restangular'];

    function ConquestCalculatorController(Restangular) {
        var vm = this;

        vm.conquestCap;
        vm.arenaRating;
        vm.bgRating;

        vm.calculate = calculate;

        activate();

        function activate() {
            vm.arenaRating = 0;
            vm.bgRating = 0;
        }

        function calculate() {
            Restangular
            .one('conquest')
            .get({
                'arenaRating': vm.arenaRating,
                'bgRating': vm.bgRating
            })
            .then(function (conquestCap) {
                vm.conquestCap = conquestCap;
            });
        }
    }
})();
