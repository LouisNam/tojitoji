(function (app) {
    app.controller('warehouseListController', warehouseListController);

    warehouseListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService'];

    function warehouseListController($scope, apiService, $ngBootbox, notificationService) {
        $scope.warehouses = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getWarehouse = getWarehouse;
        $scope.keyword = '';
        $scope.search = search;
        $scope.deleteWarehouse = deleteWarehouse;

        function deleteWarehouse(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/warehouse/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getWarehouse();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getWarehouse();
        }

        function getWarehouse(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/warehouse/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.warehouses = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load warehouse failed.');
            });
        }

        $scope.getWarehouse();
    }
})(angular.module('tojitojishop.warehouses'));