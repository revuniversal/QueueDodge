(function () {
    'use strict';

    angular
        .module('app')
        .directive('liveActivity', liveActivity);

    liveActivity.$inject = ['$interval'];

    function liveActivity($interval) {

        return {
            restrict: 'E',
            scope: {},
            bindToController: {
                bracket: '=',
                region: '=',
                data: '='
            },
            link: link,
            controller: 'LiveActivityController',
            controllerAs: 'vm',
            templateUrl: 'app/activity/live/live-activity.html',
        };

        function link($scope, element, attributes, ctrl) {

        }
    }

})();