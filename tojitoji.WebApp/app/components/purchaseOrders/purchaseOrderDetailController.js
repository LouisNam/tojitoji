(function (app) {
    app.controller('purchaseOrderDetailController', purchaseOrderDetailController);

    purchaseOrderDetailController.$inject = ['$scope', 'apiService', 'close', 'id', 'ModalService'];

    function purchaseOrderDetailController($scope, apiService, close, id, ModalService) {
        $scope.purchaseOrderDetails = [];

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

        getPurchaseOrderDetail(id);
    };
})(angular.module('tojitojishop.purchaseOrders'));