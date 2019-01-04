/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('trialbalanceListController', trialbalanceListController);

    trialbalanceListController.$inject = ['$scope', 'apiService', 'ModalService', 'notificationService'];

    function trialbalanceListController($scope, apiService, ModalService, notificationService) {
        $scope.trialbalances = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getTrialBalances = getTrialBalances;
        $scope.keyword = '';
        $scope.search = search;

        //$scope.showDetail = showDetail;

        //function showDetail(id) {

        //    ModalService.showModal({
        //        templateUrl: "/app/components/trialbalances/trialbalanceDetailView.html",
        //        controller: "trialbalanceDetailController",
        //        preClose: (modal) => { modal.element.modal('hide'); },
        //        inputs: {
        //            id: id
        //        }
        //    }).then(function (modal) {
        //        modal.element.modal();
        //        modal.close;
        //    }).catch(function (error) {
        //        // error contains a detailed error message.
        //        console.log(error);
        //    });
        //}

        function search() {
            getTrialBalances();
        }

        function getTrialBalances(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/trialbalance/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy!');
                }
                $scope.trialbalances = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load list trial balance failed.');
            });
        }

        $scope.getTrialBalances();
    }
})(angular.module('tojitojishop.trialbalances'));