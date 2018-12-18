/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('purchaseOrderDetailController', purchaseOrderDetailController);

    purchaseOrderDetailController.$inject = ['$scope', 'apiService', 'close', 'id', 'ModalService'];

    function purchaseOrderDetailController($scope, apiService, close, id, ModalService) {
        $scope.purchaseOrderDetails = [];
        $scope.showDetail = showDetail;

        $scope.close = function (result) {
            close(result, 500); // close, but give 500ms for bootstrap to animate
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
        }

        function getPurchaseOrderDetail(id) {
            apiService.get('/api/purchaseorderdetail/getdetail/' + id, null, function (result) {
                $scope.purchaseOrderDetails = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        getPurchaseOrderDetail(id);
    };
})(angular.module('tojitojishop.purchaseOrders'));