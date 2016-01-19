(function () {
    'use strict';

    angular
        .module('app')
        .directive('activityWatcher', activityWatcher);

    activityWatcher.$inject = [];

    function activityWatcher() {

        return {
            link: link,
            restrict: 'E',
            templateUrl: 'app/activity/watcher/activity-watcher.html',
            controller: 'ActivityWatcherController',
            controllerAs: 'vm',
            scope: {},
            bindToController: {
                watch: '='
            }
        };

        function link(scope, element, attrs, ctrl) {
            scope.$on('CheckWatchList', function () {
                ctrl.checkWatch();
            });

            scope.$on('Watch', function (event, data) {
                ctrl.watchPlayer(data);
            });
    
        }
    }

})();