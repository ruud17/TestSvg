'use strict';
app.filter('cutLastItem', function () {
    return function (input) {
        if (!input) return undefined;
        return _.initial(input);
    };
});