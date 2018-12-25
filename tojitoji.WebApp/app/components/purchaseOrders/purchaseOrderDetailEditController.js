(function (app) {
    app.controller('purchaseOrderDetailEditController', purchaseOrderDetailEditController);

    purchaseOrderDetailEditController.$inject = ['$scope', 'apiService', 'close', 'id', 'ModalService', 'notificationService', '$element', '$rootScope'];

    function purchaseOrderDetailEditController($scope, apiService, close, id, ModalService, notificationService, $element, $rootScope) {
        $scope.purchaseOrderDetail = {};
        $scope.UpdatePurchaseOrderDetail = UpdatePurchaseOrderDetail;

        $scope.close = function (result) {
            close(result, 500);
        };

        function UpdatePurchaseOrderDetail() {
            apiService.put('/api/purchaseorderdetail/update', $scope.purchaseOrderDetail,
                function (result) {
                    notificationService.displaySuccess('Chi tiết ' + result.data.ID + ' đã được cập nhật.');
                    $element.modal('hide');
                    $rootScope.$emit('loadDetail', $scope.purchaseOrderDetail.PurchaseOrderID);
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function loadPurchaseOrderDetail() {
            apiService.get('/api/purchaseorderdetail/getbyid/' + id, null, function (result) {
                $scope.purchaseOrderDetail = result.data;
                $scope.purchaseOrderDetail.CreatedDate = new Date(result.data.CreatedDate);
                $scope.purchaseOrderDetail.UpdatedDate = new Date(result.data.UpdatedDate);
                $scope.purchaseOrderDetail.ShippingTime = new Date(result.data.ShippingTime);
                $scope.purchaseOrderDetail.CanceledTime = new Date(result.data.CanceledTime);
                $scope.purchaseOrderDetail.DeliveriedETA = new Date(result.data.DeliveriedETA);
                $scope.purchaseOrderDetail.DeliveriedTime = new Date(result.data.DeliveriedTime);
                $scope.purchaseOrderDetail.FailedTime = new Date(result.data.FailedTime);
                $scope.purchaseOrderDetail.PaidTime = new Date(result.data.PaidTime);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function loadProduct() {
            apiService.get('api/product/getallproduct', null, function (result) {
                $scope.products = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadPurchaseOrderDetail();
        loadProduct();
    };
})(angular.module('tojitojishop.purchaseOrders'));