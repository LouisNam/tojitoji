(function (app) {
    app.controller('salesOrderDetailController', salesOrderDetailController);

    salesOrderDetailController.$inject = ['$scope', 'apiService', 'close', 'id', 'ModalService', 'notificationService'];

    function salesOrderDetailController($scope, apiService, close, id, ModalService, notificationService) {
        $scope.salesOrderDetails = [];

        $scope.close = function (result) {
            close(result, 500);
        };

        function getSalesOrderDetail(id) {
            apiService.get('/api/salesorderdetail/getbyid/' + id, null, function (result) {
                $scope.salesOrderDetails = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        getSalesOrderDetail(id);
    };
})(angular.module('tojitojishop.salesOrders'));