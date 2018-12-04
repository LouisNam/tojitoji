/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('accountListController', accountListController);

    accountListController.$inject = ['$scope', 'apiService'];

    function accountListController($scope, apiService) {
        $scope.accounts = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getAccounts = getAccounts;
        $scope.keyword = '';

        $scope.search = search;

        function search() {
            getAccounts();
        }

        function getAccounts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }
            apiService.get('/api/account/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.accounts = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
            }, function () {
                console.log('Load product failed.');
            });
        }

        $scope.getAccounts();
    }
})(angular.module('tojitojishop.accounts'));