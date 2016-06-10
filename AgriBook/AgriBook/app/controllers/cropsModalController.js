'use strict';
app.controller('cropsModalController', ['$scope', '$rootScope', '$uibModalInstance', 'cropsService', 'cloudinaryService', '$timeout', function ($scope, $rootScope, $uibModalInstance, cropsService, cloudinaryService, $timeout) {

    $scope.crops = $scope.crops || [];
    $scope.crop = $scope.crop || {};
    $scope.shouldDisableSaveButton = true;
    $scope.cropIcon = '../../content/images/icon-upload.png';
    $scope.fileUploadStarted = false;
    $scope.saveDisabled = false;
    $scope.showCropImage = false;
    $scope.hasStoredImage = false;
    $scope.imageGotDeleted = false;
    $scope.isAddMode = true;
    $scope.title = cropsService.cropTitles();
    $timeout(function () {
        $scope.selectedColor = defaultCropColor;
    }, 30);
    var isClickedOnRemoveIcon;

    $scope.removeImageFromSelectedCrop = function () {
        delete $scope.crop.ImageUrl;
        delete $scope.crop.ImageName;
        $scope.imageGotDeleted = true;
        $scope.showCropImage = false;
    };

    //original_filename

    var shouldSaveImage = function (form) {
        var hasImageFileChosen = form.elements.namedItem('file').files.length > 0 && form.elements.namedItem('file').files[0];
        var isNewRecord = !$scope.crop.Id;
        var sameImage = hasImageFileChosen && form.elements.namedItem('file').files[0].name === $scope.crop.ImageName;
        var deletedImage = $scope.hasStoredImage && $scope.imageGotDeleted;

        return !deletedImage && ((isNewRecord && hasImageFileChosen) || (!isNewRecord && !sameImage && hasImageFileChosen));
    };

    var setupCloudinaryUpload = function () {
        var form = document.getElementById('the-form');
        //   form.onsubmit = function () {
        if ($scope.pictureIsChanged) {
            if (shouldSaveImage(form)) {
                var formData = new FormData(form);

                formData.append('upload_preset', cloudinaryUploadPreset);
                formData.append('file', form.elements.namedItem('file').files[0]);

                cloudinaryService.uploadToCloudinary(formData, function (data) {
                    if (data) {
                        $scope.crop.ImageUrl = data.url;
                        $scope.crop.ImageName = form.elements.namedItem('file').files[0].name;
                    } else {
                        console.error(messages.BOS.ERROR_UPLOADING_IMAGE);
                    }
                });
            } else {
                $scope.save($scope.crop);
            }
            return false; // To avoid actual submission of the form
        }
    };

    $scope.selectCrop = function (crop, status) {
        if (!isClickedOnRemoveIcon) {
            $scope.isAddMode = status;
            if (status) {
                resetChanges();
                $scope.showCropImage = false;
            } else {
                $scope.crop = angular.copy(crop);
                if (!crop.ImageUrl) {
                    $timeout(function () {
                        $scope.showCropImage = false;
                        $scope.hasStoredImage = false;
                        $scope.imageGotDeleted = false;
                        delete $scope.selectedImageUrl;
                    });

                } else {
                    $scope.showCropImage = true;
                    $scope.selectedImageUrl = crop.ImageUrl;
                    $scope.hasStoredImage = true;
                }
            }
        }
    };

    var _loadCrops = function () {
        cropsService.getAll(function (data) {
            if (data) {
                $scope.crops = data;
            } else {
                console.error(messages.BOS.ERROR_LOADING_CROP);
            }
        });
    };

    $scope.save = function (crop) {
        if (!crop.Color) {
            crop.Color = defaultCropColor;
        }
        if (crop.Id) {
            cropsService.update(crop, function (data) {
                if (data) {
                    resetChanges();
                    $scope.showCropImage = false;
                } else {
                    console.error(messages.BOS.ERROR_UPDATING_CROP);
                }
            });
        } else {
            cropsService.save(crop, function (data) {
                if (data) {
                    resetChanges();
                    $scope.showCropImage = false;
                } else {
                    console.error(messages.BOS.ERROR_SAVING_CROP);
                }
            });
        }
        $rootScope.$broadcast('updateCrops');
        $timeout(function () {
            _loadCrops();
        }, 1000);
    };

    $scope.$watch('crop.Color', function (newVal) {
        $scope.selectedColor = newVal;
    });

    $scope.cancel = function (shouldSaveChanges) {
        shouldSaveChanges ? $uibModalInstance.close() : $uibModalInstance.dismiss('cancel');
        $rootScope.$broadcast('cropCurrentTab');
    };

    $scope.getPictureFromCloudinary = function () {
        $scope.pictureIsChanged = true;
        setupCloudinaryUpload();
    }

    function deleteCrop(crop) {
        cropsService.delete(crop, function (data) {
            if (data) {
                _loadCrops();
            } else {
                console.error(messages.BOS.CROP + crop.Name + messages.BOS.DELETE_SUCCESS_I);
            }
        });
    }

    function resetChanges() {
        $scope.crop = {};
        $scope.crop.Color = defaultCropColor;
        delete $scope.crop.ImageUrl;
        delete $scope.crop.ImageName;
    }

    $scope.setAddNewCropMode = function () {
        $scope.isAddMode = true;
    }

    $scope.removeCrop = function (selectedCrop) {
        isClickedOnRemoveIcon = true;
        deleteCrop(selectedCrop);
        $timeout(function () {
            if (selectedCrop.Id === $scope.crop.Id) {
                $scope.isAddMode = true;
                $scope.showCropImage = false;
                resetChanges();
            };
        });
        $timeout(function () {
            isClickedOnRemoveIcon = false;
        });
        $rootScope.$broadcast('updateCrops');
    }

    $scope.$watch('crop.ImageUrl', function (newVal) {
        if (typeof newVal != 'undefined') {
            $scope.showCropImage = true;
        };
    });

    $scope.$watch('crop', function (newVal) {
        if (_.isEmpty(newVal)) {
            $scope.isAddMode = true;
        }
        if (typeof newVal.Name == 'undefined' || newVal.Name === '') {
            $scope.shouldDisableSaveButton = true;
        } else {
            $scope.shouldDisableSaveButton = false;
        }
    }, true);

    _loadCrops();

}]);