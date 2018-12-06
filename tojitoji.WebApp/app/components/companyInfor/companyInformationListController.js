/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('companyInformationListController', companyInformationListController);

    companyInformationListController.$inject = ['$scope', 'apiService', 'ModalService'];

    function companyInformationListController($scope, apiService, ModalService) {
        $scope.companyInfor = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getInfor = getInfor;

        $scope.showDetail = showDetail;

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