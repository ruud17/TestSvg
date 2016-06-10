angular.module('appDirectives', [])

.directive('ngPoints', function () {
    return function(scope, element, attrs) {
        scope.$watch(attrs.ngPoints, function(value) {
            element.attr('points', value);
        });
    };
})

.directive('ngSvgFill', function () {
    return function (scope, element, attrs) {
        scope.$watch(attrs.ngSvgFill, function (value) {
            element.attr('fill', value);
        });
    };
})