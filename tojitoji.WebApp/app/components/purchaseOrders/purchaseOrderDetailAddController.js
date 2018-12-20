(function (app) {
    app.controller('purchaseOrderDetailAddController', purchaseOrderDetailAddController);

    purchaseOrderDetailAddController.$inject = ['$scope', 'apiService', 'close', 'id', 'ModalService', 'notificationService', '$element', '$rootScope'];

    function purchaseOrderDetailAddController($scope, apiService, close, id, ModalService, notificationService, $element, $rootScope) {
        $scope.purchaseOrderDetail = {
            PurchaseOrderID: id,
            Status: 'New',
            CreatedDate: new Date()
        };
        $scope.addPurchaseOrderDetail = addPurchaseOrderDetail;

        $scope.close = function (result) {
            close(result, 500);
        };

        function addPurchaseOrderDetail() {
            apiService.post('/api/purchaseorderdetail/create', $scope.purchaseOrderDetail,
                function (result) {
                    notificationService.displaySuccess('Thêm chi tiết thành công!');
                    $element.modal('hide');
                    $rootScope.$emit('loadDetail', $scope.purchaseOrderDetail.PurchaseOrderID);
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công!');
                });
        };

        function loadProduct() {
            apiService.get('api/product/getallproduct', null, function (result) {
                $scope.products = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadProduct();
    };
})(angular.module('tojitojishop.purchaseOrders'));