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
                $scope.purchaseOrderDetail.UpdatedDate = new Date();
                if ($scope.purchaseOrderDetail.ShippingTime != null) {
                    $scope.purchaseOrderDetail.ShippingTime = new Date(result.data.ShippingTime);
                }
                if ($scope.purchaseOrderDetail.CanceledTime != null) {
                    $scope.purchaseOrderDetail.CanceledTime = new Date(result.data.CanceledTime);
                }
                if ($scope.purchaseOrderDetail.DeliveriedETA != null) {
                    $scope.purchaseOrderDetail.DeliveriedETA = new Date(result.data.DeliveriedETA);
                }
                if ($scope.purchaseOrderDetail.DeliveriedTime != null) {
                    $scope.purchaseOrderDetail.DeliveriedTime = new Date(result.data.DeliveriedTime);
                }
                if ($scope.purchaseOrderDetail.FailedTime != null) {
                    $scope.purchaseOrderDetail.FailedTime = new Date(result.data.FailedTime);
                }
                if ($scope.purchaseOrderDetail.PaidTime != null) {
                    $scope.purchaseOrderDetail.PaidTime = new Date(result.data.PaidTime);
                }
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