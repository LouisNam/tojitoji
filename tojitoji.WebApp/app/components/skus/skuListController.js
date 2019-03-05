(function (app) {
    app.controller('skuListController', skuListController);

    skuListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$filter'];

    function skuListController($scope, apiService, $ngBootbox, notificationService, $filter) {
        $scope.sku = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getsku = getsku;
        $scope.keyword = '';
        $scope.search = search;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;
        $scope.exportExcel = exportExcel;

        function exportExcel() {
            apiService.get('/api/sku/ExportXls', null, function (response) {
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
                    checkedSKUs: JSON.stringify(listId)
                }
            }
            apiService.del('api/sku/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                getsku();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.sku, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.sku, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("sku", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

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