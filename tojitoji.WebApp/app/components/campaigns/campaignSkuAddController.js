(function (app) {
    app.controller('campaignSkuAddController', campaignSkuAddController);

    campaignSkuAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function campaignSkuAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.campaignsku = {};
        $scope.AddCampaignSKU = AddCampaignSKU;

        function AddCampaignSKU() {
            apiService.post('/api/campaignsku/create', $scope.campaignsku,
                function (result) {
                    notificationService.displaySuccess(result.data.ID + ' đã được thêm mới.');
                    $state.go('campaigns');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
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
    }
})(angular.module('tojitojishop.campaigns'));