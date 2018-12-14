(function (app) {
    app.controller('campaignEditController', campaignEditController);

    campaignEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function campaignEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.campaign = {};        
        $scope.UpdateCampaign = UpdateCampaign;

        function loadCampaignDetail() {
            apiService.get('/api/campaign/getbyid/' + $stateParams.id, null, function (result) {
                $scope.campaign = result.data;
                $scope.campaign.FromTime = new Date(result.data.FromTime);
                $scope.campaign.ToTime = new Date(result.data.ToTime);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateCampaign() {
            apiService.put('/api/campaign/update', $scope.campaign,
                function (result) {
                    notificationService.displaySuccess(result.data.CampaignID +' đã được cập nhật.');
                    $state.go('campaigns');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        loadCampaignDetail();
    }
})(angular.module('tojitojishop.campaigns'));