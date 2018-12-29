(function (app) {
    app.controller('salesOrderListController', salesOrderListController);

    salesOrderListController.$inject = ['$scope', 'apiService', 'ModalService', '$rootScope'];

    function salesOrderListController($scope, apiService, ModalService, $rootScope) {
        $scope.salesOrders = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getSalesOrder = getSalesOrder;
        $scope.showDetail = showDetail;

        function showDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/salesOrders/salesOrderDetailListView.html",
                controller: "salesOrderDetailListController",
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

        $rootScope.$on('loadSalesDetail', function (event, id) {
            $scope.showDetail(id);
        });

        function getSalesOrder(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 20
                }
            }
            
            $scope.loading = true;
            apiService.get('/api/salesorder/getall', config, function (result) {
                $scope.salesOrders = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load sales order failed.');
            });
        }

        $scope.getSalesOrder();
    }
})(angular.module('tojitojishop.salesOrders'));