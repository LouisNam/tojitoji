(function (app) {
    app.controller('bundleAddController', bundleAddController);

    bundleAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function bundleAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.bundle = {};
        $scope.AddBundle = AddBundle;          

        function AddBundle() {
            apiService.post('/api/bundle/create', $scope.bundle,
                function (result) {
                    notificationService.displaySuccess(result.data.BundleName + ' đã được thêm mới.');
                    $state.go('bundles');
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

        loadProduct();
    }
})(angular.module('tojitojishop.bundles'));