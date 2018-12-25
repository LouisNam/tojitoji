(function (app) {
    app.filter('paymentStatus', function () {
        return function (input) {
            if (input == true)
                return 'Đã thanh toán';
            else return 'Chưa thanh toán';
        }
    });
})(angular.module('tojitojishop.common'));