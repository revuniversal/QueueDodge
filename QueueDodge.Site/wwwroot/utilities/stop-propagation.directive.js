(function () {
    'use strict';

    angular
        .module('app')
        .directive('stopEvent', stopEvent);

         function stopEvent() {
            return {
                restrict: 'A',
                link: function (scope, element, attr) {
                    if (attr && attr.stopEvent)
                        element.bind(attr.stopEvent, function (e) {
                            e.stopPropagation();
                        });
                }
            };
        };

})();