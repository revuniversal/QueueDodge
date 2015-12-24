(function () {

    angular
        .module('app')
        .config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {

        $urlRouterProvider.otherwise("/");

        // Now set up the states
        $stateProvider
            .state('Home', {
                url: '',
                abstract: true,
                template: '<div ui-view></div>'
            })
            .state('Home.Index', {
                url: '',
                templateUrl: '/home/home-index.html',
                controller: 'HomeIndexController',
                controllerAs: 'vm'
            })
            .state('Home.Activity', {
                url: 'activity/:bracket',
                templateUrl: '/activity/activity-index.html',
                controller: 'ActivityIndexController',
                controllerAs: 'vm'
            })
            .state('Home.Conquest', {
                url: 'conquest',
                templateUrl: '/conquest-calculator/conquest-calculator.html',
                controller: 'ConquestCalculatorController',
                controllerAs: 'vm'
            })
            .state('Home.Leaderboard', {
                url: 'leaderboard',
                templateUrl: '/leaderboard/leaderboard-index.html',
                controller: 'LeaderboardIndexController',
                controllerAs: 'vm'
            });
        
    }
})();