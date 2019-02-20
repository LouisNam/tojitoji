(function () {
    angular.module('tojitojishop.coSoKinhDoanhs', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('coSoKinhDoanhs', {
            url: "/coSoKinhDoanhs",
            templateUrl: "/app/components/coSoKinhDoanhs/coSoKinhDoanhListView.html",
            controller: "coSoKinhDoanhListController"
        }).state('add_coSoKinhDoanh', {
            url: "/add_coSoKinhDoanh",
            templateUrl: "/app/components/coSoKinhDoanhs/coSoKinhDoanhAddView.html",
            controller: "coSoKinhDoanhAddController"
        }).state('edit_coSoKinhDoanh', {
            url: "/edit_coSoKinhDoanh/:id",
            templateUrl: "/app/components/coSoKinhDoanhs/coSoKinhDoanhEditView.html",
            controller: "coSoKinhDoanhEditController"
        }).state('import_coSoKinhDoanh', {
            url: "/import_coSoKinhDoanh",
            templateUrl: "/app/components/coSoKinhDoanhs/coSoKinhDoanhImportView.html",
            controller: "coSoKinhDoanhImportController"
        });
    }
})();