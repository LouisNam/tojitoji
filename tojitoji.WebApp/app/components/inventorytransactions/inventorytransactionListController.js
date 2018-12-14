(function (app) {
    app.controller('inventorytransactionListController', inventorytransactionListController);

    inventorytransactionListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService'];

    function inventorytransactionListController($scope, apiService, $ngBootbox, notificationService) {
        $scope.inventorytransactions = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getInventoryTransaction = getInventoryTransaction;
        $scope.deleteInventoryTransaction = deleteInventoryTransaction;

        function deleteInventoryTransaction(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/inventorytransaction/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getInventoryTransaction();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function getInventoryTransaction(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/inventorytransaction/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.inventorytransactions = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load inventorytransaction failed.');
            });
        }

        $scope.getInventoryTransaction();
    }
})(angular.module('tojitojishop.inventorytransactions'));