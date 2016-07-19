'use strict';

app.controller('parcelModalController', ['$scope', 'getSeason', 'getParcels', 'getCrops', 'getFertilizers', 'getWeightMetricUnits', 'getAreaMetricUnits', 'getParcel', 'plantingsService', '$uibModalInstance', 'commonService', 'parcelsService', '$rootScope', '$timeout', 'cropsService', 'fertilizersService', function ($scope, getSeason, getParcels, getCrops, getFertilizers, getWeightMetricUnits, getAreaMetricUnits, getParcel, plantingsService, $uibModalInstance, commonService, parcelsService, $rootScope, $timeout, cropsService, fertilizersService) {
    var defaultParcelColor = '#c7d9e5';

    $scope.parcels = getParcels;
    $scope.season = getSeason;
    $scope.crops = getCrops;
    $scope.fertilizers = getFertilizers;
    $scope.parcel = getParcel;
    $scope.weightMetricUnits = getWeightMetricUnits;
    $scope.areaMetricUnits = getAreaMetricUnits;
    $scope.fertilizerStatus = $scope.fertilizerStatus || { open: false };
    $scope.incomeStatus = $scope.incomeStatus || { open: false };
    $scope.pc = {};

    /*  
    *   Putting validationHelper inside the $scope 
    *   in case it needs to be used from the view.
    *   It will not be used as such in the code below
    *   to avoid writing '$scope' many time unnecessary
    */
    $scope.validationHelper = validationHelper;

    $scope.year = $scope.year || {};
    $scope.selectedAreaMetricUnit = $scope.selectedAreaMetricUnit || {};
    $scope.planting = $scope.planting || {};
    $scope.planting.PlantingCrops = $scope.planting.PlantingCrops || {};
    $scope.planting.PlantingFertilizers = $scope.planting.PlantingFertilizers || [];
    $scope.planting.Parcel = $scope.planting.Parcel || {};
    $scope.planting.Season = $scope.planting.Season || $scope.season;
    $scope.planting.Yields = $scope.planting.Yields || [];
    $scope.plantingFertilizers = $scope.plantingFertilizers || [];
    $scope.plantingCrops = $scope.plantingCrops || [];
    $scope.plantingYields = $scope.plantingYields || [];
    $scope.existingCropAmountId = $scope.existingCropAmountId || {};

    var loadParcelColor = function () {
        if (validationHelper.isCropPlanted($scope.parcel)) {
            $scope.parcelColor = validationHelper.isCropPlanted($scope.parcel) ? $scope.parcel.Plantings[0].PlantingCrops[0].Crop.Color : defaultParcelColor;
        }
    };

    loadParcelColor();

    $scope.initializeExistingCrop = function () {
        if (validationHelper.isCropPlanted($scope.parcel)) {
            for (var i = 0; i < $scope.crops.length; i++) {
                if ($scope.crops[i].Id === $scope.parcel.Plantings[0].PlantingCrops[0].Crop.Id) {
                    return $scope.crops[i];
                }
            }
        }
        return null;
    };

    $scope.initializeExistingCropQuantity = function () {
        if (validationHelper.isParcelPlanted($scope.parcel)) {
            return $scope.parcel.Plantings[0].PlantingCrops[0].Quantity;
        }
        return null;
    };

    $scope.initializeExistingCropAmountId = function () {
        if (validationHelper.isParcelPlanted($scope.parcel)) {
            return $scope.parcel.Plantings[0].PlantingCrops[0].AmountId;
        }
        return null;
    };

    var initializeCropMetricUnit = function (plantingCrops) {
        var wmu = {};
        for (var i = 0; i < $scope.weightMetricUnits.length; i++) {
            if ($scope.weightMetricUnits[i].Name === 'kg') {
                wmu = $scope.weightMetricUnits[i];
            }
        }

        for (var j = 0; j < plantingCrops.length; j++) {
            plantingCrops[j].MetricUnit = wmu;
        }

        return plantingCrops;
    };

    $scope.initializeExistingFertilizers = function () {
            for (var i = 0; i < $scope.parcel.Plantings[0].PlantingFertilizers.length; i++) {
                var existingPlantingFertilizer = {
                    AmountId: $scope.parcel.Plantings[0].PlantingFertilizers[i].AmountId,
                    Quantity: $scope.parcel.Plantings[0].PlantingFertilizers[i].Quantity,
                    MetricUnit: validationHelper.findItemById($scope.parcel.Plantings[0].PlantingFertilizers[i].MetricUnit.Id, $scope.weightMetricUnits),
                    Fertilizer: validationHelper.findItemById($scope.parcel.Plantings[0].PlantingFertilizers[i].Fertilizer.Id, $scope.fertilizers)
                };
                $scope.plantingFertilizers.push(existingPlantingFertilizer);
            }
    };

    $scope.initializeExistingYields = function () {
            for (var i = 0; i < $scope.parcel.Plantings[0].Yields.length; i++) {
                var existingPlantingYield = {
                    AmountId: $scope.parcel.Plantings[0].Yields[i].AmountId,
                    Name: $scope.parcel.Plantings[0].Yields[i].Name,
                    Quantity: $scope.parcel.Plantings[0].Yields[i].Quantity,
                    MetricUnit: validationHelper.findItemById($scope.parcel.Plantings[0].Yields[i].MetricUnit.Id, $scope.weightMetricUnits)
                };
                $scope.plantingYields.push(existingPlantingYield);
            }
    };

    $scope.initializeExistingParcelAreaMetricUnit = function () {
        return validationHelper.findItemById($scope.parcel.ParcelAreas[0].MetricUnit.Id, $scope.areaMetricUnits);
    };

    $scope.selectFertilizer = function (plantingFertilizer, fertilizer) {
        plantingFertilizer.Fertilizer = fertilizer;
    };

    $scope.selectMetricUnit = function (item, metricUnit) {
        item.MetricUnit = metricUnit;
    };

    $scope.selectCrop = function (plantingCrop, crop) {
        plantingCrop.Crop = crop;
        $scope.parcelColor = crop.Color;
    };

    var cleanupPlantingBeforeSave = function (planting) {
        for (var i = 0; i < planting.PlantingCrops.length; i++) {
            if (planting.PlantingCrops[i].MetricUnit && planting.PlantingCrops[i].MetricUnit.Amounts) {
                delete planting.PlantingCrops[i].MetricUnit.Amounts;
            }
        }

        for (var j = 0; j < planting.PlantingFertilizers.length; j++) {
            if (planting.PlantingFertilizers[j].MetricUnit && planting.PlantingFertilizers[j].MetricUnit.Amounts) {
                delete planting.PlantingFertilizers[j].MetricUnit.Amounts;
            }
        }

        for (var k = 0; k < planting.Yields.length; k++) {
            if (planting.Yields[k].MetricUnit && planting.Yields[k].MetricUnit.Amounts) {
                delete planting.Yields[k].MetricUnit.Amounts;
            }
        }

        for (var l = 0; l < planting.Parcel.ParcelAreas.length; l++) {
            if (planting.Parcel.ParcelAreas[l].MetricUnit && planting.Parcel.ParcelAreas[l].MetricUnit.Amounts) {
                delete planting.Parcel.ParcelAreas[l].MetricUnit.Amounts;
            }
        }
    };

    $scope.save = function (planting) {
        if (!formFertilizersValidity($scope.plantingFertilizers) && !formAmountsValidity($scope.plantingYields) && !formCropValidity($scope.plantingCrops[0].Crop) && $scope.parcel.GruntId !== "") {
            if ($scope.parcel.Plantings && $scope.parcel.Plantings.length > 0 && $scope.parcel.Plantings[0].Id) {
                planting.Id = $scope.parcel.Plantings[0].Id;
            }
            delete $scope.planting.Crop;
            delete $scope.parcel.Plantings;
            delete $scope.parcel.shouldMark;
            planting.Parcel = $scope.parcel;
            planting.PlantingFertilizers = $scope.plantingFertilizers;
            planting.PlantingCrops = initializeCropMetricUnit($scope.plantingCrops);
            planting.Yields = $scope.plantingYields;

            cleanupPlantingBeforeSave(planting);

            if (planting.Id) {
                plantingsService.update(planting, function (data) {
                    if (data) {
                        $scope.cancel();
                    } else {
                        console.error(messages.BOS.ERROR_SAVING_PLANTING);
                    }
                });
            } else {
                plantingsService.save(planting, function (data) {
                    if (data) {
                        $scope.cancel();
                    } else {
                        console.error(messages.BOS.ERROR_SAVING_PLANTING);
                    }
                });
            }
            $uibModalInstance.close();
            $rootScope.$broadcast('updateParcels');
        };
    };

    $scope.ok = function (planting) {
        $scope.save(planting);
    };

    $scope.cancel = function () {
        $uibModalInstance.dismiss('cancel');
    };

    $scope.openCropsModal = function () {
        commonService.openCropsModal();
    }

    $scope.openFertilizersModal = function () {
        commonService.openFertilizersModal();
    }

    function createDefaultItem(calculateMaxIdFunction, addNameProperty, addIdProperty) {
        var formObject = {
            Quantity: 0,
            MetricUnit: {}
        };

        if (addNameProperty) {
            formObject.Name = "";
        }
        if (addIdProperty) {
            formObject.Id = calculateMaxIdFunction;
        }

        return formObject;
    }

    function createNewId(array) {
        if (array.length > 0) {
            var maxId;
            _.max(array, function (item) {
                maxId = item.Id;
            });
            return maxId + 1;
        } else {
            return 1;
        }
    }

    $scope.addAnotherItem = function (array, addNameProperty, addIdProperty) {
        var newItem = createDefaultItem(createNewId(array), addNameProperty, addIdProperty);
        array.push(newItem);
    }

    $scope.removeFertilizerOrYield = function (value, selectedFertilizer) {
        var index = value.indexOf(selectedFertilizer);
        value.splice(index, 1);
    }

    //$scope.$watch('parcel', function (newVal) {
    //    $scope.selectedAreaMetricUnit = newVal.ParcelAreas[0].MetricUnit.Name;
    //});

    $scope.addAnotherItem($scope.plantingCrops, false, false);

    function getAllCrops() {
        cropsService.getAll(function (data) {
            if (data) {
                $scope.crops = data;
                initializeCropMetricUnit($scope.plantingCrops);
            } else {
                console.error(messages.BOS.ERROR_LOADING_CROP);
            }
        });
    }

    function getAllFertilizers() {
        fertilizersService.getAll(function (data) {
            if (data) {
                $scope.fertilizers = data;
            } else {
                console.error(messages.BOS.ERROR_LOADING_FERTILIZER);
            }
        });
    };

    function formFertilizersValidity(fertilizers) {
        $scope.showFertilizerErrorMessage = false;
        if (fertilizers != undefined) {
            _.each(fertilizers, function (fer) {
                if (fer.Fertilizer == undefined || fer.Fertilizer == null || fer.Quantity === "" || fer.MetricUnit.Id == undefined) {
                    $scope.showFertilizerErrorMessage = true;
                }
            });
        }
        return $scope.showFertilizerErrorMessage;
    }

    function formCropValidity(crop) {
        $scope.showCropErrorMessage = false;
        if (crop === null) {
            $scope.showCropErrorMessage = true;
        }
        return $scope.showCropErrorMessage;
    }

    function formAmountsValidity(amounts) {
        $scope.showYieldsErrorMessage = false;
        _.each(amounts, function (fer) {
            if (fer.Name === "" || fer.Quantity === "" || fer.MetricUnit.Id == undefined) {
                $scope.showYieldsErrorMessage = true;
            }
        });
        return $scope.showYieldsErrorMessage;
    }

    $scope.$on('updateCrops', function () {
        $timeout(function () {
            getAllCrops();
        }, 500);
    });

    $scope.$on('updateFertilizers', function () {
        $timeout(function () {
            getAllFertilizers();
        }, 500);
    });
}]);