(function (app) {
    app.controller('campaignSkuEditController', campaignSkuEditController);

    campaignSkuEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function campaignSkuEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.campaignsku = {};
        $scope.EditCampaignSKU = EditCampaignSKU;

        function loadCampaignSkuDetail() {
            apiService.get('/api/campaignsku/getbyid/' + $stateParams.id, null, function (result) {
                $scope.campaignsku = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function EditCampaignSKU() {
            apiService.put('/api/campaignsku/update', $scope.campaignsku,
                function (result) {
                    notificationService.displaySuccess(result.data.ID + ' đã được cập nhật.');
                    $state.go('campaigns');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function loadSKU() {
            apiService.get('api/sku/getallsku', null, function (result) {
                $scope.skus = result.data;
            }, function () {
                console.log('Cannot get list sku');
            });
        }

        loadSKU();
        loadCampaignSkuDetail();
    }
})(angular.module('tojitojishop.campaigns'));