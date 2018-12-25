/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tojitojishop.purchaseOrders', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('purchaseOrders', {
            url: "/purchaseOrders",
            templateUrl: "/app/components/purchaseOrders/purchaseOrderListView.html",
            controller: "purchaseOrderListController"
        }).state('add_purchaseOrder', {
            url: "/add_purchaseOrder",
            templateUrl: "/app/components/purchaseOrders/purchaseOrderAddView.html",
            controller: "purchaseOrderAddController"
        }).state('edit_purchaseOrder', {
            url: "/edit_purchaseOrder/:id",
            templateUrl: "/app/components/purchaseOrders/purchaseOrderEditView.html",
            controller: "purchaseOrderEditController"
        }).state('edit_purchaseOrderDetail', {
            url: "/edit_purchaseOrderDetail/:id",
            templateUrl: "/app/components/purchaseOrders/purchaseOrderDetailEditView.html",
            controller: "purchaseOrderDetailEditController"
        });
    }
})();