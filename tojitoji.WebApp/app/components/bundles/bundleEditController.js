(function (app) {
    app.controller('bundleEditController', bundleEditController);

    bundleEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function bundleEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.bundle = {};        
        $scope.UpdateBundle = UpdateBundle;

        function loadBundleDetail() {
            apiService.get('/api/bundle/getbyid/' + $stateParams.id, null, function (result) {
                $scope.bundle = result.data;
                $scope.bundle.SpecialFromTime = new Date(result.data.SpecialFromTime);
                $scope.bundle.SpecialToTime = new Date(result.data.SpecialToTime)
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateBundle() {
            apiService.put('/api/bundle/update', $scope.bundle,
                function (result) {
                    notificationService.displaySuccess(result.data.Name +' đã được cập nhật.');
                    $state.go('bundles');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
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
        loadBundleDetail();
    }
})(angular.module('tojitojishop.bundles'));