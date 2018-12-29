(function (app) {
    app.controller('transactionAddController', transactionAddController);

    transactionAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function transactionAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.transaction = {};
        $scope.AddTransaction = AddTransaction;

        function AddTransaction() {
            apiService.post('/api/transaction/create', $scope.transaction,
                function (result) {
                    notificationService.displaySuccess(result.data.ID + ' đã được thêm mới.');
                    $state.go('transactions');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
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
    }
})(angular.module('tojitojishop.transactions'));