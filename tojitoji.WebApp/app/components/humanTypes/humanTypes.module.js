/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tojitojishop.humanTypes', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('humanTypes', {
            url: "/humanTypes",
            templateUrl: "/app/components/humanTypes/humanTypeListView.html",
            controller: "humanTypeListController"
        }).state('add_humanTypes', {
            url: "/add_humanTypes",
            templateUrl: "/app/components/humanTypes/humanTypeAddView.html",
            controller: "humanTypeAddController"
        }).state('edit_humanTypes', {
            url: "/edit_humanTypes/:id",
            templateUrl: "/app/components/humanTypes/humanTypeEditView.html",
            controller: "humanTypeEditController"
        });
    }
})();