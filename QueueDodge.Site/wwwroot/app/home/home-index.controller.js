﻿(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeIndexController', HomeIndexController);

    HomeIndexController.$inject = ['$location','Restangular'];

    function HomeIndexController($location,Restangular) {
        var vm = this;
        vm.getToken = getToken;
        activate();

        function activate() {
        
        }

        function getToken(){
            var code = $location.search().code;
            Restangular
            .all("battlenet")
            .one("code", code)
            .get()
            .then(function(token){
            alert(token);
            });
        }

        function getToken() {
            var code = window.location.search.replace("?code=", "").replace("&state=", "");

            Restangular
            .all("battlenet")
            .one("oauth", code)
            .get()
            .then(function (token) {
                alert(token);
            });
        }

    }
})();