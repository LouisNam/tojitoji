(function (app) {
    app.controller('warehouseAddController', warehouseAddController);

    warehouseAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function warehouseAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.warehouse = {};
        $scope.AddWarehouse = AddWarehouse;          

        function AddWarehouse() {
            apiService.post('/api/warehouse/create', $scope.warehouse,
                function (result) {
                    notificationService.displaySuccess(result.data.ID + ' đã được thêm mới.');
                    $state.go('warehouses');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    }
})(angular.module('tojitojishop.warehouses'));