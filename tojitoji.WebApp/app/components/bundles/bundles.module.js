/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tojitojishop.bundles', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('bundles', {
            url: "/bundles",
            templateUrl: "/app/components/bundles/bundleListView.html",
            controller: "bundleListController"
        }).state('add_bundle', {
            url: "/add_bundle",
            templateUrl: "/app/components/bundles/bundleAddView.html",
            controller: "bundleAddController"
        }).state('edit_bundle', {
            url: "/edit_bundle/:id",
            templateUrl: "/app/components/bundles/bundleEditView.html",
            controller: "bundleEditController"
        });
    }
})();