(function (app) {
    app.controller('campaignAddController', campaignAddController);

    campaignAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function campaignAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.campaign = {};
        $scope.AddCampaign = AddCampaign;

        function AddCampaign() {
            apiService.post('/api/campaign/create', $scope.campaign,
                function (result) {
                    notificationService.displaySuccess(result.data.ID + ' đã được thêm mới.');
                    $state.go('campaigns');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }

        function loadCampaignSKU() {
            apiService.get('api/campaignsku/getallcampaign', null, function (result) {
                $scope.campaignSKU = result.data;
            }, function () {
                console.log('Cannot get list campaign sku');
            });
        }

        loadCampaignSKU();
    }
})(angular.module('tojitojishop.campaigns'));