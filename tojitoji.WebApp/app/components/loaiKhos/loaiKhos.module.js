(function () {
    angular.module('tojitojishop.loaiKhos', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('loaiKhos', {
            url: "/loaiKhos",
            templateUrl: "/app/components/loaiKhos/loaiKhoListView.html",
            controller: "loaiKhoListController"
        }).state('add_loaiKho', {
            url: "/add_loaiKho",
            templateUrl: "/app/components/loaiKhos/loaiKhoAddView.html",
            controller: "loaiKhoAddController"
        }).state('edit_loaiKho', {
            url: "/edit_loaiKho/:id",
            templateUrl: "/app/components/loaiKhos/loaiKhoEditView.html",
            controller: "loaiKhoEditController"
        }).state('import_loaiKho', {
            url: "/import_loaiKho",
            templateUrl: "/app/components/loaiKhos/loaiKhoImportView.html",
            controller: "loaiKhoImportController"
        });
    }
})();