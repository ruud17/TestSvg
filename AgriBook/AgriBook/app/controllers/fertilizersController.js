'use strict';
app.controller('fertilizersController', ['$scope', 'fertilizersService', function ($scope, fertilizersService) {

    $scope.fertilizer = $scope.fertilizer || {};
    $scope.fertilizers = $scope.fertilizers || {};

    $scope.getAll = function () {
        fertilizersService.getAll(function (data) {
            if (data) {
                $scope.fertilizers = data;
            } else {
                console.error(messages.BOS.ERROR_LOADING_FERTILIZER);
            }
        });
    };

    $scope.save = function (fertilizer) {
        fertilizersService.save(fertilizer, function(data) {
            if (data) {
                $scope.getAll();
            } else {
                console.error(messages.BOS.ERROR_SAVING_FERTILIZER);
            }
        });
    };

    $scope.getAll();

}]);