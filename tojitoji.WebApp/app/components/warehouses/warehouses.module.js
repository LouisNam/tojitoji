/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tojitojishop.warehouses', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('warehouses', {
            url: "/warehouses",
            templateUrl: "/app/components/warehouses/warehouseListView.html",
            controller: "warehouseListController"
        }).state('add_warehouse', {
            url: "/add_warehouse",
            templateUrl: "/app/components/warehouses/warehouseAddView.html",
            controller: "warehouseAddController"
        }).state('edit_warehouse', {
            url: "/edit_warehouse/:id",
            templateUrl: "/app/components/warehouses/warehouseEditView.html",
            controller: "warehouseEditController"
        });
    }
})();