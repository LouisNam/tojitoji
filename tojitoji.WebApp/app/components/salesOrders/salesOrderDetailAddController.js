(function (app) {
    app.controller('salesOrderDetailAddController', salesOrderDetailAddController);

    salesOrderDetailAddController.$inject = ['$scope', 'apiService', 'close', 'id', 'ModalService', 'notificationService', '$element', '$rootScope'];

    function salesOrderDetailAddController($scope, apiService, close, id, ModalService, notificationService, $element, $rootScope) {
        $scope.salesOrderDetail = {
            SalesOrderID: id,
            CreatedTime: new Date(),
            TKNGiaVon: '632',
            TKCGiaVon: '156',
            TKNGiaBan: '131',
            TKCGiaBan: '5111'
        };
        $scope.AddSalesOrderDetail = AddSalesOrderDetail;

        $scope.close = function (result) {
            close(result, 500);
        };

        function AddSalesOrderDetail() {
            apiService.post('/api/salesorderdetail/create', $scope.salesOrderDetail,
                function (result) {
                    notificationService.displaySuccess('Thêm chi tiết thành công!');
                    $element.modal('hide');
                    $rootScope.$emit('loadSalesDetail', $scope.salesOrderDetail.SalesOrderID);
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công!');
                });
        };
    };
})(angular.module('tojitojishop.salesOrders'));