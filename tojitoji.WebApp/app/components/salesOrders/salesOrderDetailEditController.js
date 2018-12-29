(function (app) {
    app.controller('salesOrderDetailEditController', salesOrderDetailEditController);

    salesOrderDetailEditController.$inject = ['$scope', 'apiService', 'close', 'id', 'ModalService', 'notificationService', '$element', '$rootScope'];

    function salesOrderDetailEditController($scope, apiService, close, id, ModalService, notificationService, $element, $rootScope) {
        $scope.salesOrderDetail = {
            UpdatedTime: new Date()
        };
        $scope.UpdateSalesOrderDetail = UpdateSalesOrderDetail;

        $scope.close = function (result) {
            close(result, 500);
        };

        function UpdateSalesOrderDetail() {
            apiService.put('/api/salesorderdetail/update', $scope.salesOrderDetail,
                function (result) {
                    notificationService.displaySuccess('Chi tiết ' + result.data.ID + ' đã được cập nhật.');
                    $element.modal('hide');
                    $rootScope.$emit('loadSalesDetail', $scope.salesOrderDetail.SalesOrderID);
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function loadPurchaseOrderDetail() {
            apiService.get('/api/salesorderdetail/getbyid/' + id, null, function (result) {
                $scope.salesOrderDetail = result.data;
                $scope.salesOrderDetail.CreatedTime = new Date(result.data.CreatedTime);
                $scope.salesOrderDetail.UpdatedTime = new Date();
                if ($scope.salesOrderDetail.ShippingTime != null) {
                    $scope.salesOrderDetail.ShippingTime = new Date(result.data.ShippingTime);
                }
                if ($scope.salesOrderDetail.CanceledTime != null) {
                    $scope.salesOrderDetail.CanceledTime = new Date(result.data.CanceledTime);
                }
                if ($scope.salesOrderDetail.DeliveriedETA != null) {
                    $scope.salesOrderDetail.DeliveriedETA = new Date(result.data.DeliveriedETA);
                }
                if ($scope.salesOrderDetail.DeliveriedTime != null) {
                    $scope.salesOrderDetail.DeliveriedTime = new Date(result.data.DeliveriedTime);
                }
                if ($scope.salesOrderDetail.FailedTime != null) {
                    $scope.salesOrderDetail.FailedTime = new Date(result.data.FailedTime);
                }
                if ($scope.salesOrderDetail.PaidTime != null) {
                    $scope.salesOrderDetail.PaidTime = new Date(result.data.PaidTime);
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        loadPurchaseOrderDetail();
    };
})(angular.module('tojitojishop.salesOrders'));