'use strict';
app.factory('plantingsService', ['$http', '$q', function ($http, $q) {

    var plantingsServiceFactory = {};

    var _getAll = function (callback) {

        var deferred = $q.defer();

        $http.get(plantingsUrl).success(function (response) {
            deferred.resolve(callback(response));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _save = function (planting, callback) {
        var deferred = $q.defer();

        $http({
            method: 'POST',
            url: plantingsUrl,
            data: planting
        }).success(function (response) {
            deferred.resolve(callback(messages.BOS.SUCCESS_SAVING_PLANTING));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _update = function (planting, callback) {
        var deferred = $q.defer();

        $http.put(plantingsUrl + '/' + planting.Id, planting).success(function (response) {
            deferred.resolve(callback(messages.BOS.SUCCESS_UPDATING_PLANTING));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    plantingsServiceFactory.getAll = _getAll;
    plantingsServiceFactory.save = _save;
    plantingsServiceFactory.update = _update;

    return plantingsServiceFactory;
}]);