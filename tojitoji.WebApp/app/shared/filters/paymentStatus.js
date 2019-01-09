(function (app) {
    app.filter('paymentStatus', function () {
        return function (input) {
            if (input == true)
                return 'Đã thanh toán';
            else if (input == false)
                return 'Chưa thanh toán';
            else
                return null;
        }
    });
})(angular.module('tojitojishop.common'));