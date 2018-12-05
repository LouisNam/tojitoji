(function (app) {
    app.filter('accountStatus', function () {
        return function (input) {
            if (input == true)
                return 'Có';
            else return null;
        }
    });
})(angular.module('tojitojishop.common'));