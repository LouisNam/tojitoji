(function (app) {
    app.controller('skuEditController', skuEditController);

    skuEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function skuEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.sku = {};        
        $scope.Updatesku = Updatesku;

        function loadskuDetail() {
            apiService.get('/api/sku/getbyid/' + $stateParams.id, null, function (result) {
                $scope.sku = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function Updatesku() {
            apiService.put('/api/sku/update', $scope.sku,
                function (result) {
                    notificationService.displaySuccess(result.data.ID +' đã được cập nhật.');
                    $state.go('skus');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function loadProduct() {
            apiService.get('api/product/getallproduct', null, function (result) {
                $scope.products = result.data;
            }, function () {
                console.log('Cannot get list product');
            });
        }

        function loadBundle() {
            apiService.get('api/bundle/getallbundle', null, function (result) {
                $scope.bundles = result.data;
            }, function () {
                console.log('Cannot get list bundle');
            });
        }

        loadProduct();
        loadBundle();
        loadskuDetail();
    }
})(angular.module('tojitojishop.skus'));