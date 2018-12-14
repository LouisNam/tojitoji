(function (app) {
    app.controller('campaignListController', campaignListController);

    campaignListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService'];

    function campaignListController($scope, apiService, $ngBootbox, notificationService) {
        $scope.campaign = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getCampaign = getCampaign;
        $scope.keyword = '';
        $scope.search = search;
        $scope.deleteCampaign = deleteCampaign;

        function deleteCampaign(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('/api/campaign/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getCampaign();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getCampaign();
        }

        function getCampaign(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/campaign/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.campaign = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load campaign failed.');
            });
        }

        $scope.getCampaign();

        $scope.campaignsku = [];
        $scope.getCampaignSKU = getCampaignSKU;
        $scope.pagesku = 0;
        $scope.pageCountSku = 0;
        $scope.keywordSku = '';
        $scope.searchSku = searchSku;
        $scope.deleteCampaignSku = deleteCampaignSku;

        function deleteCampaignSku(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/campaignsku/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getCampaignSKU();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function searchSku() {
            getCampaignSKU();
        }

        function getCampaignSKU(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/campaignsku/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.campaignsku = result.data.Items;
                $scope.pageSku = result.data.Page;
                $scope.pagesCountSku = result.data.TotalPages;
                $scope.totalCountSku = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load campaign sku failed.');
            });
        }

        $scope.getCampaignSKU();
    }
})(angular.module('tojitojishop.campaigns'));