(function (app) {
    app.controller('humanListController', humanListController);

    humanListController.$inject = ['$scope', 'apiService', 'ModalService', '$ngBootbox', 'notificationService', '$filter'];

    function humanListController($scope, apiService, ModalService, $ngBootbox, notificationService, $filter) {
        $scope.human = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getHuman = getHuman;
        $scope.showDetail = showDetail;
        $scope.deleteHuman = deleteHuman;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;
        $scope.sortColumn = "";
        $scope.reverseSort = false;
        $scope.sortData = sortData;
        $scope.getSortClass = getSortClass;
        $scope.exportExcel = exportExcel;

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedHumans: JSON.stringify(listId)
                }
            };
            apiService.del('api/human/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                getHuman();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.human, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.human, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("human", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function exportExcel() {
            apiService.get('/api/human/ExportXls', null, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }

        function sortData(column) {
            $scope.reverseSort = ($scope.sortColumn == column) ? !$scope.reverseSort : false;
            $scope.sortColumn = column;
        }

        function getSortClass(column) {
            if ($scope.sortColumn == column) {
                return $scope.reverseSort
                  ? 'glyphicon glyphicon-sort-by-attributes-alt pull-right'
                  : 'glyphicon glyphicon-sort-by-attributes pull-right';
            }
            return 'glyphicon glyphicon-sort pull-right';
        }

        function deleteHuman(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/human/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getHuman();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function showDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/humans/humanDetailView.html",
                controller: "humanDetailController",
                preClose: (modal) => { modal.element.modal('hide'); },
                inputs: {
                    id: id
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close;
            }).catch(function (error) {
                console.log(error);
            });
        }

        function getHuman(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/human/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.human = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load human failed.');
            });
        }

        $scope.getHuman();
    }
})(angular.module('tojitojishop.companyInformations'));