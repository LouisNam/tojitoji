(function (app) {
    app.controller('salesOrderAddController', salesOrderAddController);

    salesOrderAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state'];

    function salesOrderAddController($scope, apiService, notificationService, $state) {
        $scope.salesOrder = {
            CreatedDate: new Date()
        };
        $scope.AddSalesOrder = AddSalesOrder;

        function AddSalesOrder() {
            apiService.post('/api/salesorder/create', $scope.salesOrder,
                function (result) {
                    notificationService.displaySuccess('Thêm thành công!');
                    $state.go('salesOrders');
                    //location.reload();
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

        loadHumans();
        loadWarehouses();
        loadDocumentType();
    };
})(angular.module('tojitojishop.salesOrders'));