(function () {
    angular.module('tojitojishop.taiSans', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('taiSans', {
            url: "/taiSans",
            templateUrl: "/app/components/taiSans/taiSanListView.html",
            controller: "taiSanListController"
        }).state('add_taiSan', {
            url: "/add_taiSan",
            templateUrl: "/app/components/taiSans/taiSanAddView.html",
            controller: "taiSanAddController"
        }).state('edit_taiSan', {
            url: "/edit_taiSan/:id",
            templateUrl: "/app/components/taiSans/taiSanEditView.html",
            controller: "taiSanEditController"
        }).state('import_taiSan', {
            url: "/import_taiSan",
            templateUrl: "/app/components/taiSans/taiSanImportView.html",
            controller: "taiSanImportController"
        });
    }
})();