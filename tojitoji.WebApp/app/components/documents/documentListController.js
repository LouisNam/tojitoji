(function (app) {
    app.controller('documentListController', documentListController);

    documentListController.$inject = ['$scope', 'apiService', '$ngBootbox', 'notificationService'];

    function documentListController($scope, apiService, $ngBootbox, notificationService) {
        $scope.documents = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getDocument = getDocument;
        $scope.keyword = '';
        $scope.search = search;
        $scope.deleteDocument = deleteDocument;

        function deleteDocument(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/document/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getDocument();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getDocument();
        }

        function getDocument(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/document/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.documents = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load document failed.');
            });
        }

        $scope.getDocument();
    }
})(angular.module('tojitojishop.documents'));