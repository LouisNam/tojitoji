(function (app) {
    app.controller('humanTypeListController', humanTypeListController);

    humanTypeListController.$inject = ['$scope', 'apiService', 'notificationService', 'commonService', '$filter'];

    function humanTypeListController($scope, apiService, notificationService, commonService, $filter) {
        $scope.humanType = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getHumanType = getHumanType;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;
        $scope.exportExcel = exportExcel;

        function exportExcel() {
            apiService.get('/api/humanType/ExportXls', null, function (response) {
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
                    checkedHumanTypes: JSON.stringify(listId)
                }
            }
            apiService.del('api/humanType/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                getHumanType();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.humanType, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.humanType, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("humanType", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function getHumanType(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/humantype/getall', config, function (result) {
                $scope.humanType = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load human type failed.');
            });
        }

        $scope.getHumanType();
    }
})(angular.module('tojitojishop.humanTypes'));