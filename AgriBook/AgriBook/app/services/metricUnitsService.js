'use strict';
app.factory('metricUnitsService', ['$http', '$q', function ($http, $q) {

    var metricUnitsServiceFactory = {};

    var _getAll = function (callback) {

        var deferred = $q.defer();

        $http.get(metricUnitsUrl).success(function (response) {
            deferred.resolve(callback(response));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _save = function (metricUnit, callback) {
        var deferred = $q.defer();

        $http.post(metricUnitsUrl, metricUnit).success(function (response) {
            deferred.resolve(callback(messages.BOS.SUCCESS_SAVING_METRIC_UNIT));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _update = function (metricUnit, callback) {
        var deferred = $q.defer();

        $http.put(metricUnitsUrl + '/' + metricUnit.Id, metricUnit).success(function (response) {
            deferred.resolve(callback(METRIC_UNIT + metricUnit.Name + messages.BOS.EDIT_SUCCESS_F));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _delete = function (metricUnit, callback) {
        var deferred = $q.defer();

        $http.delete(metricUnitsUrl + '/' + metricUnit.Id).success(function (response) {
            deferred.resolve(callback(METRIC_UNIT + metricUnit.Name + messages.BOS.DELETE_SUCCESS_F));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _getByType = function (type, callback) {
        var deferred = $q.defer();

        $http.get(metricUnitsUrl, { params: { type: type } }).success(function (response) {
            deferred.resolve(callback(response));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    metricUnitsServiceFactory.getAll = _getAll;
    metricUnitsServiceFactory.save = _save;
    metricUnitsServiceFactory.update = _update;
    metricUnitsServiceFactory.delete = _delete;
    metricUnitsServiceFactory.getByType = _getByType;

    return metricUnitsServiceFactory;
}]);