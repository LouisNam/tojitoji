(function (app) {
    app.controller('skuListController', skuListController);

    skuListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService'];

    function skuListController($scope, apiService, $ngBootbox, notificationService) {
        $scope.sku = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getsku = getsku;
        $scope.keyword = '';
        $scope.search = search;
        $scope.deletesku = deletesku;

        function deletesku(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/sku/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getsku();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getsku();
        }

        function getsku(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/sku/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.sku = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load sku failed.');
            });
        }

        $scope.getsku();
    }
})(angular.module('tojitojishop.skus'));