(function (app) {
    app.controller('bundleListController', bundleListController);

    bundleListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService'];

    function bundleListController($scope, apiService, $ngBootbox, notificationService) {
        $scope.bundle = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getBundle = getBundle;
        $scope.keyword = '';
        $scope.search = search;
        $scope.deleteBundle = deleteBundle;

        function deleteBundle(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/bundle/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getBundle();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getBundle();
        }

        function getBundle(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/bundle/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.bundle = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load bundle failed.');
            });
        }

        $scope.getBundle();
    }
})(angular.module('tojitojishop.bundles'));