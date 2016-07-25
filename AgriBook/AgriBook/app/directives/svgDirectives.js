angular.module('appDirectives', [])

.directive('ngPoints', function () {
    return function (scope, element, attrs) {
        scope.$watch(attrs.ngPoints, function (value) {
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

.directive('blockOpeningModal', function () {
    return {
        scope: {
            shouldOpen: '='
        },
        link: function (scope, element, attr) {
            element.bind('mousedown', function () {
                scope.$apply(function () {
                    scope.shouldOpen = true;
                });
                    element.bind('mousemove', function () {
                        scope.$apply(function () {
                            scope.shouldOpen = false;
                        });
                    });
              
            });

            element.bind('mouseup',function() {
                element.unbind('mousedown',function() {
                    console.log('unbinededd');
                })
            })
        }
    };
})
