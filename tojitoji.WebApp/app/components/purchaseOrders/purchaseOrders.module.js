/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tojitojishop.purchaseOrders', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('purchaseOrders', {
            url: "/purchaseOrders",
            templateUrl: "/app/components/purchaseOrders/purchaseOrderListView.html",
            controller: "purchaseOrderListController"
        })
        //    .state('add_product', {
        //    url: "/add_product",
        //    templateUrl: "/app/components/products/productAddView.html",
        //    controller: "productAddController"
        //})
            //.state('edit_product', {
        //    url: "/edit_product/:id",
        //    templateUrl: "/app/components/products/productEditView.html",
        //    controller: "productEditController"
        //})
        ;
    }
})();