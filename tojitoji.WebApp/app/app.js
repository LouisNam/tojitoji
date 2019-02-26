(function () {
    angular.module('tojitojishop', [
        'tojitojishop.bibles',
        'tojitojishop.coSoKinhDoanhs',
        'tojitojishop.accounts',
        'tojitojishop.documenttypes',
        'tojitojishop.companyInformations',
        'tojitojishop.humans',
        'tojitojishop.humanTypes',
        'tojitojishop.categories',
        'tojitojishop.products',
        'tojitojishop.bundles',
        'tojitojishop.skus',
        'tojitojishop.campaigns',
        'tojitojishop.inventorytransactions',
        'tojitojishop.purchaseOrders',
        'tojitojishop.salesOrders',
        'tojitojishop.documents',
        'tojitojishop.transactions',
        'tojitojishop.trialbalances',
        'tojitojishop.loaiTaiSans',
        'tojitojishop.taiSans',
        'tojitojishop.khos',
        'tojitojishop.loaiKhos',
        'tojitojishop.common'
    ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/admin",
            templateUrl: "/app/components/accounts/accountListView.html",
            controller: "accountListController"
        });
        $urlRouterProvider.otherwise('/admin');
    }
})();