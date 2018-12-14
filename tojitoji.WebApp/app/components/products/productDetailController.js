/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productDetailController', productDetailController);

    productDetailController.$inject = ['$scope', 'apiService', 'close', 'id'];

    function productDetailController($scope, apiService, close, id) {
        $scope.product = [];

        $scope.close = function (result) {
            close(result, 500); // close, but give 500ms for bootstrap to animate
        };

        function getProductDetail(id) {
            apiService.get('/api/product/getbyid/' + id, null, function (result) {
                $scope.product = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        getProductDetail(id);
    };
})(angular.module('tojitojishop.products'));