(function () {
    angular.module('tojitojishop.skus', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('skus', {
            url: "/skus",
            templateUrl: "/app/components/skus/skuListView.html",
            controller: "skuListController"
        }).state('add_sku', {
            url: "/add_sku",
            templateUrl: "/app/components/skus/skuAddView.html",
            controller: "skuAddController"
        }).state('edit_sku', {
            url: "/edit_sku/:id",
            templateUrl: "/app/components/skus/skuEditView.html",
            controller: "skuEditController"
        }).state('import_sku', {
            url: "/import_sku",
            templateUrl: "/app/components/skus/skuImportView.html",
            controller: "skuImportController"
        });
    }
})();