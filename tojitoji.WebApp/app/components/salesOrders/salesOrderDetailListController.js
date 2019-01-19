(function (app) {
    app.controller('salesOrderDetailListController', salesOrderDetailListController);

    salesOrderDetailListController.$inject = ['$scope', 'apiService', 'close', 'id', 'ModalService', '$ngBootbox', 'notificationService', '$rootScope', '$element'];

    function salesOrderDetailListController($scope, apiService, close, id, ModalService, $ngBootbox, notificationService, $rootScope, $element) {
        $scope.salesOrderDetails = [];
        $scope.showDetail = showDetail;
        $scope.addSalesOrderDetail = addSalesOrderDetail;
        $scope.salesOrderID = id;
        $scope.editSalesOrderDetail = editSalesOrderDetail;
        $scope.deleteSalesOrderDetail = deleteSalesOrderDetail;
        $scope.returnSalesOrderDetail = returnSalesOrderDetail;

        $scope.close = function (result) {
            close(result, 500);
        };

        function showDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/salesOrders/salesOrderDetailView.html",
                controller: "salesOrderDetailController",
                preClose: (modal) => { modal.element.modal('hide'); },
                inputs: {
                    id: id
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close;
            }).catch(function (error) {
                console.log(error);
            });
        };

        function deleteSalesOrderDetail(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/salesorderdetail/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công!');
                    $element.modal('hide');
                    $rootScope.$emit('loadSalesDetail', $scope.salesOrderID);
                }, function () {
                    notificationService.displayError('Xóa không thành công!');
                })
            });
        };

        function getSalesOrderDetailList(id) {
            apiService.get('/api/salesorderdetail/getdetail/' + id, null, function (result) {
                $scope.salesOrderDetails = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        };

        function addSalesOrderDetail(salesOrderID) {
            ModalService.showModal({
                templateUrl: "/app/components/salesOrders/salesOrderDetailAddView.html",
                controller: "salesOrderDetailAddController",
                preClose: (modal) => { modal.element.modal('hide'); },
                inputs: {
                    id: salesOrderID
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close;
            }).catch(function (error) {
                console.log(error);
            });
        };

        function editSalesOrderDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/salesOrders/salesOrderDetailEditView.html",
                controller: "salesOrderDetailEditController",
                preClose: (modal) => { modal.element.modal('hide'); },
                inputs: {
                    id: id
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close;
            }).catch(function (error) {
                console.log(error);
            });
        };

        function returnSalesOrderDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/salesOrders/salesOrderDetailReturnAddView.html",
                controller: "salesOrderDetailReturnAddController",
                preClose: (modal) => { modal.element.modal('hide'); },
                inputs: {
                    id: id
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close;
            }).catch(function (error) {
                console.log(error);
            });
        };

        getSalesOrderDetailList(id);
    };
})(angular.module('tojitojishop.salesOrders'));