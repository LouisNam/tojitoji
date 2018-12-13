(function (app) {
    app.controller('categoryListController', categoryListController);

    categoryListController.$inject = ['$scope', 'apiService', 'ModalService', '$ngBootbox', 'notificationService'];

    function categoryListController($scope, apiService, ModalService, $ngBootbox, notificationService) {
        $scope.category = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getCategory = getCategory;
        $scope.keyword = '';
        $scope.search = search;

        $scope.showDetail = showDetail;

        $scope.deleteCategory = deleteCategory;

        function deleteCategory(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/category/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getCategory();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function showDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/category/categoryDetailView.html",
                controller: "categoryDetailController",
                preClose: (modal) => { modal.element.modal('hide'); },
                inputs: {
                    id: id
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close;
            }).catch(function (error) {
                console.log(error);
            });
        }

        function search() {
            getCategory();
        }

        function getCategory(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/category/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.category = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load category failed.');
            });
        }

        $scope.getCategory();
    }
})(angular.module('tojitojishop.categories'));