/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tojitojishop.inventorytransactions', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('inventorytransactions', {
            url: "/inventorytransactions",
            templateUrl: "/app/components/inventorytransactions/inventorytransactionListView.html",
            controller: "inventorytransactionListController"
        }).state('add_inventorytransaction', {
            url: "/add_inventorytransaction",
            templateUrl: "/app/components/inventorytransactions/inventorytransactionAddView.html",
            controller: "inventorytransactionAddController"
        }).state('edit_inventorytransaction', {
            url: "/edit_inventorytransaction/:id",
            templateUrl: "/app/components/inventorytransactions/inventorytransactionEditView.html",
            controller: "inventorytransactionEditController"
        });
    }
})();