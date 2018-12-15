/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('documenttypeListController', documenttypeListController);

    documenttypeListController.$inject = ['$scope', 'apiService', 'ModalService', 'notificationService'];

    function documenttypeListController($scope, apiService, ModalService, notificationService) {
        $scope.documenttypes = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getDocumentTypes = getDocumentTypes;        

        function getDocumentTypes(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/documenttype/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                    notificationService.displayWarning('Không có bản ghi nào được tìm thấy!');
                }
                $scope.documenttypes = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load document type failed.');
            });
        }

        $scope.getDocumentTypes();
    }
})(angular.module('tojitojishop.documenttypes'));