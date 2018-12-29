(function (app) {
    app.controller('transactionListController', transactionListController);

    transactionListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService'];

    function transactionListController($scope, apiService, $ngBootbox, notificationService) {
        $scope.transactions = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getTransaction = getTransaction;
        $scope.keyword = '';
        $scope.search = search;
        $scope.deleteTransaction = deleteTransaction;

        function deleteTransaction(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/transaction/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getTransaction();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getTransaction();
        }

        function getTransaction(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/transaction/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.transactions = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load transaction failed.');
            });
        }

        $scope.getTransaction();
    }
})(angular.module('tojitojishop.transactions'));