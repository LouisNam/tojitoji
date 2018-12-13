(function () {
    angular.module('tojitojishop.campaigns', ['tojitojishop.common']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider']

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('campaigns', {
            url: "/campaigns",
            templateUrl: "/app/components/campaigns/campaignListView.html",
            controller: "campaignListController"
        }).state('add_campaign', {
            url: "/add_campaign",
            templateUrl: "/app/components/campaigns/campaignAddView.html",
            controller: "campaignAddController"
        }).state('add_campaign_sku', {
            url: "/add_campaign_sku",
            templateUrl: "/app/components/campaigns/campaignSkuAddView.html",
            controller: "campaignSkuAddController"
        }).state('edit_campaign_sku', {
            url: "/edit_campaign_sku/:id",
            templateUrl: "/app/components/campaigns/campaignSkuEditView.html",
            controller: "campaignSkuEditController"
        }).state('edit_campaign', {
            url: "/edit_campaign/:id",
            templateUrl: "/app/components/campaigns/campaignEditView.html",
            controller: "campaignEditController"
        });
    }
})();