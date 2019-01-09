(function (app) {
    app.controller('purchaseOrderDetailController', purchaseOrderDetailController);

    purchaseOrderDetailController.$inject = ['$scope', 'apiService', 'close', 'id', 'ModalService', 'notificationService'];

    function purchaseOrderDetailController($scope, apiService, close, id, ModalService, notificationService) {
        $scope.purchaseOrderDetails = [];
        $scope.purchaseOrderDetailReturns = [];

        $scope.close = function (result) {
            close(result, 500);
        };

        function getPurchaseOrderDetail(id) {
            apiService.get('/api/purchaseorderdetail/getbyid/' + id, null, function (result) {
                $scope.purchaseOrderDetails = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function getPurchaseOrderDetailReturn(id) {
            apiService.get('/api/purchaseorderdetailreturn/getbyid/' + id, null, function (result) {
                $scope.purchaseOrderDetailReturns = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        getPurchaseOrderDetail(id);
        getPurchaseOrderDetailReturn(id);
    };
})(angular.module('tojitojishop.purchaseOrders'));