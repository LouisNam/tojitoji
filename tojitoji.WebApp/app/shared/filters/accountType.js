(function (app) {
    app.filter('accountType', function () {
        return function (input) {
            if (input == true)
                return 'Có';
            else if (input == false)
                return 'Nợ';
            else return null;
        }
    });
})(angular.module('tojitojishop.common'));