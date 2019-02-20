(function (app) {
    app.controller('coSoKinhDoanhListController', coSoKinhDoanhListController);

    coSoKinhDoanhListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$filter'];

    function coSoKinhDoanhListController($scope, apiService, $ngBootbox, notificationService, $filter) {
        $scope.coSoKinhDoanhs = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getCoSoKinhDoanh = getCoSoKinhDoanh;
        $scope.deleteCoSoKinhDoanh = deleteCoSoKinhDoanh;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;
        $scope.exportExcel = exportExcel;

        function exportExcel() {
            apiService.get('/api/cosokinhdoanh/ExportXls', null, function (response) {
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
                    checkedCoSoKinhDoanhs: JSON.stringify(listId)
                }
            }
            apiService.del('api/cosokinhdoanh/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                getCoSoKinhDoanh();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.coSoKinhDoanhs, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.coSoKinhDoanhs, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("coSoKinhDoanhs", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteCoSoKinhDoanh(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/cosokinhdoanh/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getCoSoKinhDoanh();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function getCoSoKinhDoanh(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/cosokinhdoanh/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy!');
                }
                $scope.coSoKinhDoanhs = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load coSoKinhDoanh failed.');
            });
        }

        $scope.getCoSoKinhDoanh();
    }
})(angular.module('tojitojishop.coSoKinhDoanhs'));