(function (app) {
    app.controller('taiSanListController', taiSanListController);

    taiSanListController.$inject = ['$scope', 'apiService', 'notificationService', '$filter'];

    function taiSanListController($scope, apiService, notificationService, $filter) {
        $scope.taiSans = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getTaiSan = getTaiSan;
        $scope.keyword = '';
        $scope.search = search;
        $scope.exportExcel = exportExcel;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedTaiSans: JSON.stringify(listId)
                }
            }
            apiService.del('api/taisan/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                getTaiSan();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.taiSans, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.taiSans, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("taiSans", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function exportExcel() {
            apiService.get('/api/taisan/ExportXls', null, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }

        function search() {
            getTaiSan();
        }

        function getTaiSan(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/taisan/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.taiSans = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load tai san failed.');
            });
        }

        $scope.getTaiSan();
    }
})(angular.module('tojitojishop.taiSans'));