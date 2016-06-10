'use strict';
app.filter('limitText', ['$filter', function ($filter) {
    return function (input, limit) {
        if (!input) return undefined;
        if (input.length <= limit) {
            return input;
        }
        return $filter('limitTo')(input, limit) + '...';
    };
}]);