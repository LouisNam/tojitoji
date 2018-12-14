/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tojitojishop.products', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('products', {
            url: "/products",
            templateUrl: "/app/components/products/productListView.html",
            controller: "productListController"
        }).state('add_product', {
            url: "/add_product",
            templateUrl: "/app/components/products/productAddView.html",
            controller: "productAddController"
        }).state('edit_product', {
            url: "/edit_product/:id",
            templateUrl: "/app/components/products/productEditView.html",
            controller: "productEditController"
        });
    }
})();