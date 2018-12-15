/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tojitojishop.documenttypes', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('documenttypes', {
            url: "/documenttypes",
            templateUrl: "/app/components/documenttypes/documenttypeListView.html",
            controller: "documenttypeListController"
        })
    }
})();