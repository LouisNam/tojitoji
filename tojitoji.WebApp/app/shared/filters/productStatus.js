(function (app) {
    app.filter('productStatus', function () {
        return function (input) {
            if (input == true)
                return 'Active';
            else return 'Inactive';
        }
    });
})(angular.module('tojitojishop.common'));