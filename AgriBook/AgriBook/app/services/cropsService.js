'use strict';
app.factory('cropsService', ['$http', '$q', function ($http, $q) {

    var _cropTitles = function () {
        var title = {
            addMode: 'Dodaj novo sjeme',
            editMode: 'Uredi sjeme'
        }
        return title;
    };

    var cropsServiceFactory = {};

    var _selectedCrop = _selectedCrop || {};

    var _setSelectedCrop = function (selectedCrop) {
        _selectedCrop = selectedCrop;
    };

    var _getSelectedCrop = function () {
        return _selectedCrop;
    };

    var _getAll = function (callback) {

        var deferred = $q.defer();

        $http.get(cropsUrl).success(function (response) {
            deferred.resolve(callback(response));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _save = function (crop, callback) {
        var deferred = $q.defer();

        $http.post(cropsUrl, crop).success(function (response) {
            deferred.resolve(callback(messages.BOS.SUCCESS_SAVING_CROP));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _update = function (crop, callback) {
        var deferred = $q.defer();

        $http.put(cropsUrl + '/' + crop.Id, crop).success(function (response) {
            deferred.resolve(callback(messages.BOS.CROP + crop.Name + messages.BOS.EDIT_SUCCESS_I));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _delete = function (crop, callback) {
        var deferred = $q.defer();

        $http.delete(cropsUrl + '/' + crop.Id).success(function (response) {
            deferred.resolve(callback(messages.BOS.CROP + crop.Name + messages.BOS.DELETE_SUCCESS_I));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    cropsServiceFactory.getAll = _getAll;
    cropsServiceFactory.save = _save;
    cropsServiceFactory.update = _update;
    cropsServiceFactory.delete = _delete;
    cropsServiceFactory.setSelectedCrop = _setSelectedCrop;
    cropsServiceFactory.getSelectedCrop = _getSelectedCrop;
    cropsServiceFactory.cropTitles = _cropTitles;

    return cropsServiceFactory;
}]);