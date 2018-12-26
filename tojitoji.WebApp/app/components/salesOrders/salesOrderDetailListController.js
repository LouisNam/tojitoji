(function (app) {
    app.controller('salesOrderDetailListController', salesOrderDetailListController);

    salesOrderDetailListController.$inject = ['$scope', 'apiService', 'close', 'id', 'ModalService', '$ngBootbox', 'notificationService', '$rootScope', '$element'];

    function salesOrderDetailListController($scope, apiService, close, id, ModalService, $ngBootbox, notificationService, $rootScope, $element) {
        $scope.salesOrderDetails = [];
        $scope.showDetail = showDetail;
        //$scope.addPurchaseOrderDetail = addPurchaseOrderDetail;
        //$scope.purchaseOrderID = id;
        //$scope.deletePurchaseOrderDetail = deletePurchaseOrderDetail;
        //$scope.editPurchaseOrderDetail = editPurchaseOrderDetail;

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

        //function deletePurchaseOrderDetail(id) {
        //    $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
        //        var config = {
        //            params: {
        //                id: id
        //            }
        //        }
        //        apiService.del('api/purchaseorderdetail/delete', config, function () {
        //            notificationService.displaySuccess('Xóa thành công!');
        //            $element.modal('hide');
        //            $rootScope.$emit('loadDetail', $scope.purchaseOrderID);
        //        }, function () {
        //            notificationService.displayError('Xóa không thành công!');
        //        })
        //    });
        //};

        function getSalesOrderDetailList(id) {
            apiService.get('/api/salesorderdetail/getdetail/' + id, null, function (result) {
                $scope.salesOrderDetails = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        };

        //function addPurchaseOrderDetail(purchaseOrderID) {
        //    ModalService.showModal({
        //        templateUrl: "/app/components/purchaseOrders/purchaseOrderDetailAddView.html",
        //        controller: "purchaseOrderDetailAddController",
        //        preClose: (modal) => { modal.element.modal('hide'); },
        //        inputs: {
        //            id: purchaseOrderID
        //        }
        //    }).then(function (modal) {
        //        modal.element.modal();
        //        modal.close;
        //    }).catch(function (error) {
        //        console.log(error);
        //    });
        //};

        //function editPurchaseOrderDetail(id) {
        //    ModalService.showModal({
        //        templateUrl: "/app/components/purchaseOrders/purchaseOrderDetailEditView.html",
        //        controller: "purchaseOrderDetailEditController",
        //        preClose: (modal) => { modal.element.modal('hide'); },
        //        inputs: {
        //            id: id
        //        }
        //    }).then(function (modal) {
        //        modal.element.modal();
        //        modal.close;
        //    }).catch(function (error) {
        //        console.log(error);
        //    });
        //};

        getSalesOrderDetailList(id);
    };
})(angular.module('tojitojishop.salesOrders'));