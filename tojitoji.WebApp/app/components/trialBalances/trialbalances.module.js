(function () {
    angular.module('tojitojishop.trialbalances', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('trialbalances', {
            url: "/trialbalances",
            templateUrl: "/app/components/trialBalances/trialbalanceListView.html",
            controller: "trialbalanceListController"
        })
    }
})();