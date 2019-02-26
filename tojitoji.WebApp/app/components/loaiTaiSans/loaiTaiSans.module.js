(function () {
    angular.module('tojitojishop.loaiTaiSans', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('loaiTaiSans', {
            url: "/loaiTaiSans",
            templateUrl: "/app/components/loaiTaiSans/loaiTaiSanListView.html",
            controller: "loaiTaiSanListController"
        }).state('add_loaiTaiSan', {
            url: "/add_loaiTaiSan",
            templateUrl: "/app/components/loaiTaiSans/loaiTaiSanAddView.html",
            controller: "loaiTaiSanAddController"
        }).state('edit_loaiTaiSan', {
            url: "/edit_loaiTaiSan/:id",
            templateUrl: "/app/components/loaiTaiSans/loaiTaiSanEditView.html",
            controller: "loaiTaiSanEditController"
        }).state('import_loaiTaiSan', {
            url: "/import_loaiTaiSan",
            templateUrl: "/app/components/loaiTaiSans/loaiTaiSanImportView.html",
            controller: "loaiTaiSanImportController"
        });
    }
})();