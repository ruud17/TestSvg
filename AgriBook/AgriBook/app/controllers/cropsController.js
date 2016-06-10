'use strict';
app.controller('cropsController', ['$scope', 'cropsService', function ($scope, cropsService) {

    $scope.crop = $scope.crop || {};
    $scope.crops = $scope.crops || {};

    $scope.getAll = function () {
        cropsService.getAll(function (data) {
            if (data) {
                $scope.crops = data;
            } else {
                console.error(messages.BOS.ERROR_LOADING_CROP);
            }
        });
    };

    $scope.save = function (crop) {
        cropsService.save(crop, function (data) {
            if (data) {
                $scope.getAll();
            } else {
                console.error(messages.BOS.ERROR_SAVING_CROP);
            }
        });
    };

    $scope.getAll();

}]);