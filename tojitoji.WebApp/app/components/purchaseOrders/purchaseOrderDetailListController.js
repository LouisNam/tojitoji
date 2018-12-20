(function (app) {
    app.controller('purchaseOrderDetailListController', purchaseOrderDetailListController);

    purchaseOrderDetailListController.$inject = ['$scope', 'apiService', 'close', 'id', 'ModalService'];

    function purchaseOrderDetailListController($scope, apiService, close, id, ModalService) {
        $scope.purchaseOrderDetails = [];
        $scope.showDetail = showDetail;
        $scope.addPurchaseOrderDetail = addPurchaseOrderDetail;
        $scope.id = id;

        $scope.close = function (result) {
            close(result, 500);
        };

        function showDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/purchaseOrders/purchaseOrderDetailView.html",
                controller: "purchaseOrderDetailController",
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

        function getPurchaseOrderDetailList(id) {
            apiService.get('/api/purchaseorderdetail/getdetail/' + id, null, function (result) {
                $scope.purchaseOrderDetails = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        };

        function addPurchaseOrderDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/purchaseOrders/purchaseOrderDetailAddView.html",
                controller: "purchaseOrderDetailAddController",
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

        getPurchaseOrderDetailList(id);
    };
})(angular.module('tojitojishop.purchaseOrders'));