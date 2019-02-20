(function (app) {
    app.controller('companyInformationListController', companyInformationListController);

    companyInformationListController.$inject = ['$scope', 'apiService', 'ModalService', '$ngBootbox', 'notificationService', '$filter'];

    function companyInformationListController($scope, apiService, ModalService, $ngBootbox, notificationService, $filter) {
        $scope.companyInfor = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getInfor = getInfor;
        $scope.showDetail = showDetail;
        $scope.deleteCompanyInformation = deleteCompanyInformation;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;
        $scope.exportExcel = exportExcel;

        function exportExcel() {
            apiService.get('/api/companyinformation/ExportXls', null, function (response) {
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
                    checkedCompanyInformations: JSON.stringify(listId)
                }
            }
            apiService.del('api/companyinformation/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                getInfor();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.companyInfor, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.companyInfor, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("companyInfor", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function deleteCompanyInformation(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/companyinformation/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getInfor();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function showDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/companyInfor/companyInformationDetailView.html",
                controller: "companyInformationDetailController",
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

        function getInfor(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/companyinformation/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.companyInfor = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load company information failed.');
            });
        }

        $scope.getInfor();
    }
})(angular.module('tojitojishop.companyInformations'));