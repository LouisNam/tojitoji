/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('purchaseOrderListController', purchaseOrderListController);

    purchaseOrderListController.$inject = ['$scope', 'apiService', 'ModalService', '$ngBootbox', 'notificationService'];

    function purchaseOrderListController($scope, apiService, ModalService, $ngBootbox, notificationService) {
        $scope.purchaseOrder = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getPurchaseOrder = getPurchaseOrder;

        $scope.showDetail = showDetail;

        //$scope.deletepurchaseOrder = deletepurchaseOrder;

        //function deletepurchaseOrder(id) {
        //    $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
        //        var config = {
        //            params: {
        //                id: id
        //            }
        //        }
        //        apiService.del('api/purchaseOrder/delete', config, function () {
        //            notificationService.displaySuccess('Xóa thành công');
        //            getpurchaseOrder();
        //        }, function () {
        //            notificationService.displayError('Xóa không thành công');
        //        })
        //    });
        //}

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
                if (result.data.TotalCount == 0) {
                }
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