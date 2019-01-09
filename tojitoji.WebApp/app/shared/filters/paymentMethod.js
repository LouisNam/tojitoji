(function (app) {
    app.filter('paymentMethod', function () {
        return function (input) {
            if (input == true)
                return 'Tiền mặt';
            else if (input == true)
                return 'Chuyển khoản';
            else
                return null;
        }
    });
})(angular.module('tojitojishop.common'));