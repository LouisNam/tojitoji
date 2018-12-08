/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('tojitojishop.humans', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('humans', {
            url: "/humans",
            templateUrl: "/app/components/humans/humanListView.html",
            controller: "humanListController"
        }).state('add_humans', {
            url: "/add_humans",
            templateUrl: "/app/components/humans/humanAddView.html",
            controller: "humanAddController"
        }).state('edit_humans', {
            url: "/edit_humans/:id",
            templateUrl: "/app/components/humans/humanEditView.html",
            controller: "humanEditController"
        });
    }
})();