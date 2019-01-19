(function (app) {
    app.controller('salesOrderDetailReturnAddController', salesOrderDetailReturnAddController);

    salesOrderDetailReturnAddController.$inject = ['$scope', 'apiService', 'close', 'id', 'notificationService', '$rootScope', '$state', '$element'];

    function salesOrderDetailReturnAddController($scope, apiService, close, id, notificationService, $rootScope, $state, $element) {
        $scope.salesOrderDetailReturn = {
            TKNGiaVon: '156',
            TKCGiaVon: '632',
            TKNGiaBan: '531',
            TKCGiaBan: '131',
            DocumentTypeID: 'NT',
            SalesOrderReturnID: id,
            ID: id
        };
        $scope.AddSalesOrderDetailReturn = AddSalesOrderDetailReturn;

        $scope.close = function (result) {
            close(result, 500);
        };

        function AddSalesOrderDetailReturn() {
            apiService.post('/api/salesorderdetailreturn/create', $scope.salesOrderDetailReturn,
                function (result) {
                    notificationService.displaySuccess('Thêm thành công!');
                    $element.modal('hide');
                    location.reload();
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công!');
                });
        };
    };
})(angular.module('tojitojishop.salesOrders'));