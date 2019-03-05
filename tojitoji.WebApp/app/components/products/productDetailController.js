(function (app) {
    app.controller('productDetailController', productDetailController);

    productDetailController.$inject = ['$scope', 'apiService', 'close', 'id'];

    function productDetailController($scope, apiService, close, id) {
        $scope.product = [];

        $scope.close = function (result) {
            close(result, 500);
        };

        function getProductDetail(id) {
            apiService.get('/api/product/getbyid/' + id, null, function (result) {
                $scope.product = result.data;
                if ($scope.product.MoreImage == null || $scope.product.MoreImage == "") {
                    $scope.moreImages = [];
                } else {
                    $scope.moreImages = JSON.parse($scope.product.MoreImage);
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        getProductDetail(id);
    };
})(angular.module('tojitojishop.products'));