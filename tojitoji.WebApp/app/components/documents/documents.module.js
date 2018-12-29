(function () {
    angular.module('tojitojishop.documents', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('documents', {
            url: "/documents",
            templateUrl: "/app/components/documents/documentListView.html",
            controller: "documentListController"
        }).state('add_document', {
            url: "/add_document",
            templateUrl: "/app/components/documents/documentAddView.html",
            controller: "documentAddController"
        }).state('edit_document', {
            url: "/edit_document/:id",
            templateUrl: "/app/components/documents/documentEditView.html",
            controller: "documentEditController"
        });
    }
})();