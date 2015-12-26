(function () {
    'use strict';

    angular.module('app',
    ['ui.router',
    'ui.bootstrap',
    'restangular',
    'angularMoment',
    'ngAnimate']);

    angular.module('app')
    .constant('_', window._);

    angular
        .module('app')
        .config(config);

    config.$inject = ['RestangularProvider', '$locationProvider'];

    function config(RestangularProvider, $locationProvider) {

        RestangularProvider.setBaseUrl('/api');

    }
    
})();