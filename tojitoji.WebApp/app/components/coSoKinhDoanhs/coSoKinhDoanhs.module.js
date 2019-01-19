(function () {
    angular.module('tojitojishop.coSoKinhDoanhs', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('coSoKinhDoanhs', {
            url: "/coSoKinhDoanhs",
            templateUrl: "/app/components/coSoKinhDoanhs/coSoKinhDoanhListView.html",
            controller: "coSoKinhDoanhListController"
        });
    }
})();