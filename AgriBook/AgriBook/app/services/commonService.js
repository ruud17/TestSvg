'use strict';
app.factory('commonService', ['$uibModal', function ($uibModal) {

    function openParcelModal(season, parcels, crops, fertilizers, weightMetricUnits, areaMetricUnits, parcel) {
        $uibModal.open({
            templateUrl: '../views/partials/parcelModal.html',
            controller: 'parcelModalController',
            size: 'lg',
            backdrop: 'static',
            resolve: {
                getSeason: function() {
                    return season;
                },
                getParcels: function () {
                    return parcels;
                },
                getCrops: function () {
                    return crops;
                },
                getFertilizers: function () {
                    return fertilizers;
                },
                getWeightMetricUnits: function () {
                    return weightMetricUnits;
                },
                getAreaMetricUnits: function () {
                    return areaMetricUnits;
                },
                getParcel: function () {
                    return parcel;
                }
            }
        });
    };

    function openCropsModal() {
        $uibModal.open({
            templateUrl: '../views/partials/cropsModal.html',
            controller: 'cropsModalController',
            size: 'lg',
            backdrop: 'static'
        });
    };

    function openFertilizersModal() {
        $uibModal.open({
            templateUrl: '../views/partials/fertilizersModal.html',
            controller: 'fertilizersModalController',
            size: 'lg',
            backdrop: 'static'
        });
    };

    return {
        openParcelModal: openParcelModal,
        openCropsModal: openCropsModal,
        openFertilizersModal: openFertilizersModal
    };
}]);