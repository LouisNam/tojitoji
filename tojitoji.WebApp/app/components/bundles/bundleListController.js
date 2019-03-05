(function (app) {
    app.controller('bundleListController', bundleListController);

    bundleListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$filter'];

    function bundleListController($scope, apiService, $ngBootbox, notificationService, $filter) {
        $scope.bundles = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getBundle = getBundle;
        $scope.keyword = '';
        $scope.search = search;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;
        $scope.exportExcel = exportExcel;

        function exportExcel() {
            apiService.get('/api/bundle/ExportXls', null, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedBundles: JSON.stringify(listId)
                }
            }
            apiService.del('api/bundle/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                getBundle();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.bundles, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.bundles, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("bundles", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

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
                $scope.bundles = result.data.Items;
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