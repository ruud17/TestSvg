'use strict';
app.factory('fertilizersService', ['$http', '$q', function ($http, $q) {

    var fertilizersServiceFactory = {};

    var _fertilizerTitles = function() {
        var title = {
            addMode: 'Dodaj novo gnojivo',
            editMode: 'Uredi gnojivo'
        }
        return title;
    };

    var _selectedFertilizer = _selectedFertilizer || {};

    var _setSelectedFertilizer = function (selectedFertilizer) {
        _selectedFertilizer = selectedFertilizer;
    };

    var _getSelectedFertilizer = function () {
        return _selectedFertilizer;
    };

    var _getAll = function (callback) {

        var deferred = $q.defer();

        $http.get(fertilizersUrl).success(function (response) {
            deferred.resolve(callback(response));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;

    };

    var _save = function (fertilizer, callback) {
        var deferred = $q.defer();

        $http.post(fertilizersUrl, fertilizer).success(function (response) {
            deferred.resolve(callback(messages.BOS.SUCCESS_SAVING_FERTILIZER));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _update = function (fertilizer, callback) {
        var deferred = $q.defer();

        $http.put(fertilizersUrl + '/' + fertilizer.Id, fertilizer).success(function (response) {
            deferred.resolve(callback(messages.BOS.FERTILIZER + fertilizer.Name + messages.BOS.EDIT_SUCCESS_I));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _delete = function (fertilizer, callback) {
        var deferred = $q.defer();

        $http.delete(fertilizersUrl + '/' + fertilizer.Id).success(function (response) {
            deferred.resolve(callback(messages.BOS.FERTILIZER + fertilizer.Name + messages.BOS.DELETE_SUCCESS_I));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    }

    fertilizersServiceFactory.getAll = _getAll;
    fertilizersServiceFactory.save = _save;
    fertilizersServiceFactory.update = _update;
    fertilizersServiceFactory.delete = _delete;
    fertilizersServiceFactory.setSelectedFertilizer = _setSelectedFertilizer;
    fertilizersServiceFactory.getSelectedFertilizer = _getSelectedFertilizer;
    fertilizersServiceFactory.fertilizerTitles = _fertilizerTitles;

    return fertilizersServiceFactory;
}]);