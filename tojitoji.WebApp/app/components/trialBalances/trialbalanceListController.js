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
        $scope.sortColumn = "";
        $scope.reverseSort = false;
        $scope.sortData = sortData;
        $scope.getSortClass = getSortClass;

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