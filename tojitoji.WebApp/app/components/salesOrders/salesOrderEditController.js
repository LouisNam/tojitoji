(function (app) {
    app.controller('salesOrderEditController', salesOrderEditController);

    salesOrderEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams'];

    function salesOrderEditController($scope, apiService, notificationService, $state, $stateParams) {
        $scope.salesOrder = {};
        $scope.UpdateSalesOrder = UpdateSalesOrder;

        function loadSalesOrderDetail() {
            apiService.get('/api/salesorder/getbyid/' + $stateParams.id, null, function (result) {
                $scope.salesOrder = result.data;
                $scope.salesOrder.CreatedDate = new Date(result.data.CreatedDate);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateSalesOrder() {
            apiService.post('/api/salesorder/create', $scope.salesOrder,
                function (result) {
                    notificationService.displaySuccess('Thêm thành công!');
                    $state.go('salesOrders');
                    location.reload();
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công!');
                });
        };

        function loadDocumentType() {
            apiService.get('api/documenttype/getalldocumenttype', null, function (result) {
                $scope.documentTypes = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        function loadHumans() {
            apiService.get('api/human/getallhumans', null, function (result) {
                $scope.humans = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        function loadWarehouses() {
            apiService.get('api/warehouse/getallwarehouse', null, function (result) {
                $scope.warehouses = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadSalesOrderDetail();
        loadHumans();
        loadWarehouses();
        loadDocumentType();
    };
})(angular.module('tojitojishop.salesOrders'));