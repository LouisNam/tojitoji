(function () {
    angular.module('tojitojishop.companyInformations', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('companyInformations', {
            url: "/companyInformations",
            templateUrl: "/app/components/companyInfor/companyInformationListView.html",
            controller: "companyInformationListController"
        }).state('add_companyInformations', {
            url: "/add_companyInformations",
            templateUrl: "/app/components/companyInfor/companyInformationAddView.html",
            controller: "companyInformationAddController"
        }).state('edit_companyInformations', {
            url: "/edit_companyInformations/:id",
            templateUrl: "/app/components/companyInfor/companyInformationEditView.html",
            controller: "companyInformationEditController"
        }).state('import_companyInformations', {
            url: "/import_companyInformations",
            templateUrl: "/app/components/companyInfor/companyInformationImportView.html",
            controller: "companyInformationImportController"
        });
    }
})();