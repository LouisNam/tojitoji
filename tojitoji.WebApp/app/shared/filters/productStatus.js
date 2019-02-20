(function (app) {
    app.filter('productStatus', function () {
        return function (input) {
            if (input == true)
                return 'Active';
            else if (input == false)
                return 'Inactive';
            else return null;
        }
    });
})(angular.module('tojitojishop.common'));