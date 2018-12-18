/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tojitojishop', [
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
        'tojitojishop.warehouses',
        'tojitojishop.inventorytransactions',
        'tojitojishop.purchaseOrders',
        'tojitojishop.common'
    ]).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/admin",
            templateUrl: "/app/components/home/homeView.html",
            controller: "homeController"
        });
        $urlRouterProvider.otherwise('/admin');
    }
})();