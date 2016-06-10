var app = window.angular.module('app', [
    'ngRoute',
    'appDirectives',
    'LocalStorageModule',
    'ui.bootstrap',
    'colorpicker.module'
]);

app.config([
    '$routeProvider', '$httpProvider', function($routeProvider, $httpProvider) {
        $httpProvider.defaults.useXDomain = true;
        delete $httpProvider.defaults.headers.common['X-Requested-With'];

        $routeProvider.when("/home", {
            controller: "homeController",
            templateUrl: "../views/home.html"
        });

        $routeProvider.when("/login", {
            controller: "loginController",
            templateUrl: "../views/login.html"
        });

        $routeProvider.when("/parcels", {
            controller: "parcelsController",
            templateUrl: "../views/parcels.html"
        });

        $routeProvider.when("/fertilizers", {
            controller: "fertilizersController",
            templateUrl: "../views/fertilizers.html"
        });

        $routeProvider.when("/crops", {
            controller: "cropsController",
            templateUrl: "../views/crops.html"
        });

        $routeProvider.otherwise({ redirectTo: "/login" });

        function isValidDate(d) {
            if (Object.prototype.toString.call(d) !== "[object Date]")
                return false;
            return !isNaN(d.getTime());
        };

        var param = function(obj) {
            var query = '', name, value, fullSubName, subName, subValue, innerObj, i;

            for (name in obj) {
                if (name.indexOf("$$hash") > -1) {
                    continue;
                }

                value = obj[name];

                if (value instanceof Array) {
                    for (i = 0; i < value.length; ++i) {
                        subValue = value[i];
                        fullSubName = name + '[' + i + ']';
                        innerObj = {};
                        innerObj[fullSubName] = subValue;
                        query += param(innerObj) + '&';
                    }
                } else if (value instanceof Object && !isValidDate(value)) {
                    for (subName in value) {
                        subValue = value[subName];
                        fullSubName = name + '[' + subName + ']';
                        innerObj = {};
                        innerObj[fullSubName] = subValue;
                        query += param(innerObj) + '&';
                    }
                } else if (isValidDate(value)) {
                    query += encodeURIComponent(name) + '=' + encodeURIComponent(JSON.parse(JSON.stringify(value))) + '&';
                } else if (value !== undefined && value !== null)
                    query += encodeURIComponent(name) + '=' + encodeURIComponent(value) + '&';
            }

            return query.length ? query.substr(0, query.length - 1) : query;
        };

        // Override $http service's default transformRequest
        //$httpProvider.defaults.transformRequest = [function (data) {
        //    var transformedData = param(data);
        //    transformedData = transformedData.replace(/&&+/g, "&");
        //    return angular.isObject(data) && String(data) !== '[object File]' ? transformedData : data;
        //}];

        $httpProvider.interceptors.push('authInterceptorService');
    }
]).run(function($rootScope, $location, authService) {
    authService.fillAuthData();

    $rootScope.$on("$routeChangeStart", function() {
        if ($location.url() === '/login') {
            document.getElementsByTagName("body")[0].setAttribute("id", "site-bg");
        } else {
            document.getElementsByTagName("body")[0].removeAttribute("id");
        } 
    });
});


