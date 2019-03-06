(function (app) {
    app.controller('accountListController', accountListController);

    accountListController.$inject = ['$scope', 'apiService', 'ModalService', 'notificationService'];

    function accountListController($scope, apiService, ModalService, notificationService) {
        $scope.accounts = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getAccounts = getAccounts;
        $scope.keyword = '';
        $scope.search = search;
        $scope.showDetail = showDetail;

        $('#field_search').keypress(function (event) {
            if (event.keyCode == 13) {
                $('#btn_search').click();
            }
        });

        function showDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/accounts/accountDetailView.html",
                controller: "accountDetailController",
                preClose: (modal) => { modal.element.modal('hide'); },
                inputs: {
                    id: id
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close;
            }).catch(function (error) {
                // error contains a detailed error message.
                console.log(error);
            });
        }

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

            $scope.loading = true;
            apiService.get('/api/account/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy!');
                }
                $scope.accounts = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load account failed.');
            });
        }

        $scope.getAccounts();
    }
})(angular.module('tojitojishop.accounts'));