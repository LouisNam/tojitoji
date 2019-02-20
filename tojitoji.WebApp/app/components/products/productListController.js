(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'ModalService', '$ngBootbox', 'notificationService'];

    function productListController($scope, apiService, ModalService, $ngBootbox, notificationService) {
        $scope.product = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getProduct = getProduct;
        $scope.keyword = '';
        $scope.search = search;
        $scope.showDetail = showDetail;
        $scope.deleteProduct = deleteProduct;
        $scope.sortColumn = "";
        $scope.reverseSort = false;
        $scope.sortData = sortData;
        $scope.getSortClass = getSortClass;

        function sortData(column) {
            $scope.reverseSort = ($scope.sortColumn == column) ? !$scope.reverseSort : false;
            $scope.sortColumn = column;
        }

        function getSortClass(column) {
            if ($scope.sortColumn == column) {
                return $scope.reverseSort
                  ? 'glyphicon glyphicon-sort-by-attributes-alt pull-right'
                  : 'glyphicon glyphicon-sort-by-attributes pull-right';
            }
            return 'glyphicon glyphicon-sort pull-right';
        }

        function deleteProduct(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/product/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getProduct();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function search() {
            getProduct();
        }

        function showDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/products/productDetailView.html",
                controller: "productDetailController",
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

        function getProduct(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/product/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.product = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load product failed.');
            });
        }

        $scope.getProduct();
    }
})(angular.module('tojitojishop.products'));