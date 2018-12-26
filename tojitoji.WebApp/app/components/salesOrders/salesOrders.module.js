/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tojitojishop.salesOrders', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('salesOrders', {
            url: "/salesOrders",
            templateUrl: "/app/components/salesOrders/salesOrderListView.html",
            controller: "salesOrderListController"
        }).state('add_salesOrder', {
            url: "/add_salesOrder",
            templateUrl: "/app/components/salesOrders/salesOrderAddView.html",
            controller: "salesOrderAddController"
        }).state('edit_salesOrder', {
            url: "/edit_salesOrder/:id",
            templateUrl: "/app/components/salesOrders/salesOrderEditView.html",
            controller: "salesOrderEditController"
        })
            //.state('edit_purchaseOrderDetail', {
        //    url: "/edit_purchaseOrderDetail/:id",
        //    templateUrl: "/app/components/purchaseOrders/purchaseOrderDetailEditView.html",
        //    controller: "purchaseOrderDetailEditController"
        //})
        ;
    }
})();