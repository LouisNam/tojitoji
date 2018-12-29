(function (app) {
    app.controller('transactionEditController', transactionEditController);

    transactionEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function transactionEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.transaction = {};
        $scope.UpdateTransaction = UpdateTransaction;

        function loadTransactionDetail() {
            apiService.get('/api/transaction/getbyid/' + $stateParams.id, null, function (result) {
                $scope.transaction = result.data;
                if ($scope.transaction.Date != null) {
                    $scope.transaction.Date = new Date(result.data.Date);
                }
                if ($scope.transaction.BillDate != null) {
                    $scope.transaction.BillDate = new Date(result.data.BillDate);
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateTransaction() {
            apiService.put('/api/transaction/update', $scope.transaction,
                function (result) {
                    notificationService.displaySuccess(result.data.ID +' đã được cập nhật.');
                    $state.go('transactions');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function loadHumans() {
            apiService.get('api/human/getallhumans', null, function (result) {
                $scope.humans = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        function loadDocuments() {
            apiService.get('api/document/getalldocument', null, function (result) {
                $scope.documents = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        function loadAccounts() {
            apiService.get('api/account/getallaccount', null, function (result) {
                $scope.accounts = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadAccounts();
        loadDocuments();
        loadHumans();
        loadTransactionDetail();
    }
})(angular.module('tojitojishop.transactions'));