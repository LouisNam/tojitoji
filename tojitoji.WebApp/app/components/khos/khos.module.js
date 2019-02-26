(function () {
    angular.module('tojitojishop.khos', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('khos', {
            url: "/khos",
            templateUrl: "/app/components/khos/khoListView.html",
            controller: "khoListController"
        }).state('add_kho', {
            url: "/add_kho",
            templateUrl: "/app/components/khos/khoAddView.html",
            controller: "khoAddController"
        }).state('edit_kho', {
            url: "/edit_kho/:id",
            templateUrl: "/app/components/khos/khoEditView.html",
            controller: "khoEditController"
        }).state('import_kho', {
            url: "/import_kho",
            templateUrl: "/app/components/khos/khoImportView.html",
            controller: "khoImportController"
        });
    }
})();