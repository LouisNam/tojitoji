(function (app) {
    app.filter('paymentMethod', function () {
        return function (input) {
            if (input == true)
                return 'Tiền mặt';
            else return 'Chuyển khoản';
        }
    });
})(angular.module('tojitojishop.common'));