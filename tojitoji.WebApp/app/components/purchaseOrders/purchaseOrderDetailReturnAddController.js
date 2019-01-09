(function (app) {
    app.controller('purchaseOrderDetailReturnAddController', purchaseOrderDetailReturnAddController);

    purchaseOrderDetailReturnAddController.$inject = ['$scope', 'apiService', 'close', 'id', 'notificationService', '$rootScope', '$state', '$element'];

    function purchaseOrderDetailReturnAddController($scope, apiService, close, id, notificationService, $rootScope, $state, $element) {
        $scope.purchaseOrderDetailReturn = {
            TKN: '331',
            TKC: '156',
            DocumentTypeID: 'XT',
            PurchaseOrderReturnID: id,
            ID: id
        };
        $scope.AddPurchaseOrderDetailReturn = AddPurchaseOrderDetailReturn;

        $scope.close = function (result) {
            close(result, 500);
        };

        function AddPurchaseOrderDetailReturn() {
            apiService.post('/api/purchaseorderdetailreturn/create', $scope.purchaseOrderDetailReturn,
                function (result) {
                    notificationService.displaySuccess('Thêm thành công!');
                    $element.modal('hide');
                    location.reload();
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công!');
                });
        };
    };
})(angular.module('tojitojishop.purchaseOrders'));