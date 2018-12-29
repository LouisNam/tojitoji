(function () {
    angular.module('tojitojishop.transactions', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('transactions', {
            url: "/transactions",
            templateUrl: "/app/components/transactions/transactionListView.html",
            controller: "transactionListController"
        }).state('add_transaction', {
            url: "/add_transaction",
            templateUrl: "/app/components/transactions/transactionAddView.html",
            controller: "transactionAddController"
        }).state('edit_transaction', {
            url: "/edit_transaction/:id",
            templateUrl: "/app/components/transactions/transactionEditView.html",
            controller: "transactionEditController"
        });
    }
})();