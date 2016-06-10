'use strict';
app.factory('parcelsService', ['$http', '$q', function ($http, $q) {

    var parcelsServiceFactory = {};
    var _selectedParcel = _selectedParcel || {};

    var _setSelectedParcel = function(selectedParcel) {
        _selectedParcel = selectedParcel;
    };

    var _getSelectedParcel = function() {
        return _selectedParcel;
    };

    var _getAll = function (year, callback) {
        var deferred = $q.defer();

        $http.get(parcelsUrl, { params: { plantingYear: year } }).success(function (response) {
            deferred.resolve(callback(response));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _get = function (id, callback) {
        var deferred = $q.defer();

        $http.get(parcelsUrl, { params: { id: id, includePlantings: true } }).success(function (response) {
            deferred.resolve(callback(response));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    parcelsServiceFactory.getAll = _getAll;
    parcelsServiceFactory.get = _get;
    parcelsServiceFactory.setSelectedParcel = _setSelectedParcel;
    parcelsServiceFactory.getSelectedParcel = _getSelectedParcel;

    return parcelsServiceFactory;
}]);