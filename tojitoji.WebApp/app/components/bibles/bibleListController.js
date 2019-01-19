(function (app) {
    app.controller('bibleListController', bibleListController);

    bibleListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService', '$filter'];

    function bibleListController($scope, apiService, $ngBootbox, notificationService, $filter) {
        $scope.bibles = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getBible = getBible;
        $scope.deleteBible = deleteBible;
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
                    checkedBibles: JSON.stringify(listId)
                }
            }
            apiService.del('api/bible/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                getBible();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.bibles, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.bibles, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("bibles", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteBible(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/bible/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getBible();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function getBible(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/bible/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy!');
                }
                $scope.bibles = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load bible failed.');
            });
        }

        $scope.getBible();
    }
})(angular.module('tojitojishop.bibles'));