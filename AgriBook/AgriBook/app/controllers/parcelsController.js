'use strict';
app.controller('parcelsController', ['$scope', 'parcelsService', '$uibModal', '$timeout', 'commonService', 'cropsService', 'fertilizersService', 'metricUnitsService', function ($scope, parcelsService, $uibModal, $timeout, commonService, cropsService, fertilizersService, metricUnitsService) {

    $scope.season = season;

    $timeout(function () {
        var panZoomInstance = svgPanZoom('#svg-id', {
            zoomEnabled: true,
            controlIconsEnabled: true,
            fit: true,
            center: true,
            dblClickZoomEnabled: true,
            minZoom: 1
        });
        panZoomInstance.zoom(0.2);
    }, 500);

    $scope.parcels = $scope.parcels || {};
    $scope.parcel = $scope.parcel || {};
    $scope.plantingSeason = Date.now();
    $scope.crops = $scope.crops || {};
    $scope.fertilizers = $scope.fertilizers || {};
    $scope.weightMetricUnits = $scope.weightMetricUnits || {};
    $scope.areaMetricUnits = $scope.areaMetricUnits || {};
    $scope.currentTab = 1;
    $scope.shouldSearch = false;

    $scope.getAll = function (season) {
        parcelsService.getAll(season, function (data) {
            if (data) {
                $scope.parcels = data;
                $scope.resetSearch();
            } else {
                console.error(messages.BOS.ERROR_LOADING_PARCELS);
            }
        });
    };

    $scope.get = function (id) {
        parcelsService.get(id, function (data) {
            if (data) {
                $scope.parcel = data;
            } else {
                console.error(messages.BOS.ERROR_LOADING_PARCEL);
            }
        });
    };

    var loadCrops = function () {
        cropsService.getAll(function (data) {
            if (data) {
                $scope.crops = data;
            } else {
                console.error(messages.BOS.ERROR_LOADING_CROP);
            }
        });
    };

    var loadMetricUnits = function () {
        metricUnitsService.getAll(function (data) {
            if (data) {
                $scope.weightMetricUnits = $.grep(data, function (e) { return e.Type === 1 });
                $scope.areaMetricUnits = $.grep(data, function (e) { return e.Type === 2 });
            } else {
                console.error(messages.BOS.ERROR_LOADING_METRIC_UNITS);
            }
        });
    };

    var loadFertilizers = function () {
        fertilizersService.getAll(function (data) {
            if (data) {
                $scope.fertilizers = data;
            } else {
                console.error(messages.BOS.ERROR_LOADING_FERTILIZER);
            }
        });
    };

    var loadData = function (year) {
        $scope.getAll(year);
        loadCrops();
        loadFertilizers();
        loadMetricUnits();
    };

    $scope.selectSeason = function (season) {
        var date = new Date(Date.now());
        date.setYear(season);
        $scope.selectedSeason = date;
        loadData(season);
    };

    $scope.openParcelModal = function (parcel) {
        parcelsService.get(parcel.Id, function (data) {
            if (data) {
                $scope.parcel = data;
                commonService.openParcelModal($scope.selectedSeason, $scope.parcels, $scope.crops, $scope.fertilizers, $scope.weightMetricUnits, $scope.areaMetricUnits, $scope.parcel);
                $scope.selectedParcel = parcel;
                parcelsService.setSelectedParcel($scope.selectedParcel);
            } else {
                console.error(messages.BOS.ERROR_LOADING_PARCEL);
            }
        });
    };

    $scope.openCropsModal = function () {
        commonService.openCropsModal();
    };

    $scope.openFertilizersModal = function () {
        commonService.openFertilizersModal();
    };

    function plantingPreview(parcels) {
        var crops = [], foundCrop;
        var parcelsWithPlantings = _.reject(parcels, function (parcel) { return parcel.Plantings.length === 0; });
        _.each(parcelsWithPlantings, function (parcel) {
            var planting = {
                cropId: parcel.Plantings[0].PlantingCrops[0].Crop.Id,
                ParcelIds: [parcel.Id],
                Name: parcel.Plantings[0].PlantingCrops[0].Crop.Name,
                ImageUrl: parcel.Plantings[0].PlantingCrops[0].Crop.ImageUrl
            };

            foundCrop = _.findWhere(crops, { cropId: planting.cropId });
            if (typeof foundCrop != 'undefined') {
                _.each(crops, function (crop, ind) {
                    if (crop.cropId === foundCrop.cropId) {
                        crops[ind].ParcelIds.push(planting.ParcelIds[0]);
                    }
                });
            } else {
                crops.push(planting);
            }

        });
        return crops;
    }

    $scope.calculatePercentage = function (parcels, item) {
        var sum = 0;
        _.each(parcels, function (par) {
            sum += par.ParcelIds.length;
        });
        return (item.ParcelIds.length / sum) * 100;
    };

    $scope.searchById = function (parcelValue) {
        if (parcelValue !== "") {
            _.each($scope.parcels, function (p, ind) {
                if (p.GruntId.toLowerCase() === parcelValue.toLowerCase()) {
                    $scope.parcels[ind].shouldMark = true;
                } else {
                    $scope.parcels[ind].shouldMark = false;
                }
                $scope.shouldSearch = true;
            });
        } else {
            _.each($scope.parcels, function (p, ind) {
                $scope.parcels[ind].shouldMark = false;
            });
            $scope.shouldSearch = false;
        }
    }

    $scope.resetSearch = function() {
        _.each($scope.parcels, function (parcel, i) {
            $scope.parcels[i].shouldMark = false;
        });
        $scope.searchParcelValue = "";
    };

    $scope.searchByName = function (parcelValue) {
        if (parcelValue !== "") {
            _.each($scope.parcels, function (p, ind) {
                if (p.Name.toLowerCase().indexOf(parcelValue.toLowerCase()) !== -1) {
                    $scope.parcels[ind].shouldMark = true;
                } else {
                    $scope.parcels[ind].shouldMark = false;
                }
                $scope.shouldSearch = true;
            });
        } else {
            _.each($scope.parcels, function (p, ind) {
                $scope.parcels[ind].shouldMark = false;
            });
            $scope.shouldSearch = false;
        }
    }

    $scope.searchByCrop = function (parcelValue) {
        if (parcelValue !== "") {
            _.each($scope.parcels, function (p, ind) {
                if (p.Plantings.length) {
                    if (p.Plantings[0].PlantingCrops[0].Crop.Name.toLowerCase() === parcelValue.toLowerCase()) {
                        $scope.parcels[ind].shouldMark = true;
                    } else {
                        $scope.parcels[ind].shouldMark = false;
                    }
                } else {
                    $scope.parcels[ind].shouldMark = false;
                }
                $scope.shouldSearch = true;
            });
        } else {
            _.each($scope.parcels, function (p, ind) {
                $scope.parcels[ind].shouldMark = false;
            });
            $scope.shouldSearch = false;
        }
    }


    //#region datepicker

    $scope.format = 'yyyy';

    $scope.popup = {
        opened: false
    };

    $scope.dateOptions = {
        formatYear: 'yyyy',
        startingDay: 1,
        minMode: 'year'
    };

    $scope.open = function () {
        $scope.popup.opened = true;
    };

    $scope.year = new Date();

    $scope.$watch('year', function (newVal) {
        if (typeof newVal != 'undefined' && newVal != null) {
            $scope.selectSeason(newVal.getFullYear());
            $timeout(function () {
                $scope.parcelsPreview = plantingPreview($scope.parcels);
            }, 2500);
        }
    }, true);

    //#endregion datepicker

    $scope.$on('updateParcels', function () {
        $timeout(function () {
            $scope.currentTab = 1;
            $scope.getAll($scope.year.getFullYear());
        }, 500);

        $timeout(function () {
            $scope.parcelsPreview = plantingPreview($scope.parcels);
        }, 2500);
    });

    $scope.$on('updateCrops', function () {
        $timeout(function () {
            $scope.currentTab = 1;
            loadCrops();
        }, 500);
    });

    $scope.$on('cropCurrentTab', function () {
        $scope.currentTab = 1;
    });

    $scope.$on('fertilizerCurrentTab', function () {
        $scope.currentTab = 1;
    });
}]);