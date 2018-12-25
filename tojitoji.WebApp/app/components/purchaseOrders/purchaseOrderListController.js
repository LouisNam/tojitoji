(function (app) {
    app.controller('purchaseOrderListController', purchaseOrderListController);

    purchaseOrderListController.$inject = ['$scope', 'apiService', 'ModalService', '$rootScope'];

    function purchaseOrderListController($scope, apiService, ModalService, $rootScope) {
        $scope.purchaseOrder = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getPurchaseOrder = getPurchaseOrder;
        $scope.showDetail = showDetail;

        function showDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/purchaseOrders/purchaseOrderDetailListView.html",
                controller: "purchaseOrderDetailListController",
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

        $rootScope.$on('loadDetail', function (event, id) {
            $scope.showDetail(id);
        });

        function getPurchaseOrder(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 20
                }
            }
            
            $scope.loading = true;
            apiService.get('/api/purchaseOrder/getall', config, function (result) {
                $scope.purchaseOrder = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load purchase order failed.');
            });
        }

        $scope.getPurchaseOrder();
    }
})(angular.module('tojitojishop.purchaseOrders'));