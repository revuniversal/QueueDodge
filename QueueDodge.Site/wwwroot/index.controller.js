(function () {
    'use strict';

    angular
        .module('app')
        .controller('IndexController', IndexController);

    IndexController.$inject = ['$scope','Restangular', '$interval'];

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



        }

    }
})();
