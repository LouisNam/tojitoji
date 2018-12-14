(function (app) {
    app.controller('skuAddController', skuAddController);

    skuAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function skuAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.sku = {};
        $scope.AddSKU = AddSKU;          

        function AddSKU() {
            apiService.post('/api/sku/create', $scope.sku,
                function (result) {
                    notificationService.displaySuccess(result.data.ID + ' đã được thêm mới.');
                    $state.go('skus');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }

        function loadProduct() {
            apiService.get('api/product/getallproduct', null, function (result) {
                $scope.products = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        function loadBundle() {
            apiService.get('api/bundle/getallbundle', null, function (result) {
                $scope.bundles = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadBundle();
        loadProduct();
    }
})(angular.module('tojitojishop.skus'));