'use strict';
app.factory('cloudinaryService', ['$http', '$q', function ($http, $q) {

    var cloudinaryServiceFactory = {};

    var _uploadToCloudinary = function (formData, callback) {
        var deferred = $q.defer();
        
        $http.post(cloudinaryUrl, formData, {
            headers: {
                'Content-Type': undefined,
                'X-Requested-With': 'XMLHttpRequest',
                'Authorization': undefined
            }
        }).success(function (cloudinaryResponse) {
            deferred.resolve(callback(cloudinaryResponse));
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    cloudinaryServiceFactory.uploadToCloudinary = _uploadToCloudinary;

    return cloudinaryServiceFactory;
}]);