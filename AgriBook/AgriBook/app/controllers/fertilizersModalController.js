'use strict';
app.controller('fertilizersModalController', ['$scope', 'fertilizersService', '$uibModalInstance', '$rootScope', '$timeout', function ($scope, fertilizersService, $uibModalInstance, $rootScope, $timeout) {

    angular.extend($scope, {
        fertilizers: [],
        isAddMode: true,
        shouldDisableSaveButton: true,
        currentFertilizer: {},
        title: fertilizersService.fertilizerTitles()
    });

    var isClickedOnRemoveIcon;

    function loadData() {
        getAllFertilizers();
    }

    function getAllFertilizers() {
        fertilizersService.getAll(function (data) {
            if (data) {
                $scope.fertilizers = data;
            } else {
                console.error(messages.BOS.ERROR_LOADING_FERTILIZER);
            }
        });
    }

    function addNewFertilizer(fertilizer) {
        fertilizersService.save(fertilizer, function (data) {
            if (data) {
                getAllFertilizers();
            } else {
                console.error(messages.BOS.ERROR_SAVING_FERTILIZER);
            }
        });
    }

    function updateFertilizer(fertilizer) {
        fertilizersService.update(fertilizer, function (data) {
            if (data) {
                getAllFertilizers();
            } else {
                console.error(messages.BOS.ERROR_SAVING_FERTILIZER);
            }
        });
    }

    function deleteFertilizer(fertilizer) {
        fertilizersService.delete(fertilizer, function (data) {
            if (data) {
                getAllFertilizers();
            } else {
                console.error(messages.BOS.FERTILIZER + fertilizer.Name + messages.BOS.DELETE_SUCCESS_I);
            }
        });
    }

    function resetChanges() {
        $scope.currentFertilizer = {};
    };

    $scope.closeModal = function (shouldSaveChanges) {
        shouldSaveChanges ? $uibModalInstance.close() : $uibModalInstance.dismiss('cancel');
        $rootScope.$broadcast('fertilizerCurrentTab');
    };

    $scope.setMode = function (selectedFertilizer, status) {
        if (!isClickedOnRemoveIcon) {
            $scope.isAddMode = status;
            if (status) {
                resetChanges();
            } else {
                $scope.currentFertilizer = angular.copy(selectedFertilizer);
            }
        }
    }

    $scope.save = function () {
        if ($scope.isAddMode) {
            addNewFertilizer($scope.currentFertilizer);
        } else {
            updateFertilizer($scope.currentFertilizer);
        }
        $rootScope.$broadcast('updateFertilizers');
        resetChanges();
    };

    $scope.removeFertilizer = function (selectedFertilizer) {
        isClickedOnRemoveIcon = true;
        deleteFertilizer(selectedFertilizer);
        if (selectedFertilizer.Id === $scope.currentFertilizer.Id) {
            resetChanges();
        };
        $timeout(function () {
            isClickedOnRemoveIcon = false;
        });
    }

    $scope.$watch('currentFertilizer', function (newVal) {
        if (_.isEmpty(newVal)) {
            $scope.isAddMode = true;
        }
        if (typeof newVal.Name == 'undefined' || newVal.Name === '') {
            $scope.shouldDisableSaveButton = true;
        } else {
            $scope.shouldDisableSaveButton = false;
        }
    }, true);

    loadData();

}]);