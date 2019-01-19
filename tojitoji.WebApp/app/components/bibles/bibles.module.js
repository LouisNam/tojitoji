(function () {
    angular.module('tojitojishop.bibles', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('bibles', {
            url: "/bibles",
            templateUrl: "/app/components/bibles/bibleListView.html",
            controller: "bibleListController"
        }).state('add_bible', {
            url: "/add_bible",
            templateUrl: "/app/components/bibles/bibleAddView.html",
            controller: "bibleAddController"
        }).state('edit_bible', {
            url: "/edit_bible/:id",
            templateUrl: "/app/components/bibles/bibleEditView.html",
            controller: "bibleEditController"
        }).state('import_bible', {
            url: "/import_bible",
            templateUrl: "/app/components/bibles/bibleImportView.html",
            controller: "bibleImportController"
        });
    }
})();