(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['$scope', 'apiService', 'ModalService', '$ngBootbox', 'notificationService', '$filter'];

    function productListController($scope, apiService, ModalService, $ngBootbox, notificationService, $filter) {
        $scope.product = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getProduct = getProduct;
        $scope.keyword = '';
        $scope.search = search;
        $scope.showDetail = showDetail;
        $scope.sortColumn = "";
        $scope.reverseSort = false;
        $scope.sortData = sortData;
        $scope.getSortClass = getSortClass;
        $scope.exportExcel = exportExcel;
        $scope.selectAll = selectAll;
        $scope.isAll = false;
        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var listId = [];
            $.each($scope.selected, function (i, item) {
                listId.push(item.ID);
            });
            var config = {
                params: {
                    checkedProducts: JSON.stringify(listId)
                }
            }
            apiService.del('api/product/deletemulti', config, function (result) {
                notificationService.displaySuccess('Xóa thành công ' + result.data + ' bản ghi.');
                getProduct();
            }, function (error) {
                notificationService.displayError('Xóa không thành công');
            });
        }

        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.product, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.product, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("product", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        function exportExcel() {
            apiService.get('/api/product/ExportXls', null, function (response) {
                if (response.status = 200) {
                    window.location.href = response.data.Message;
                }
            }, function (error) {
                notificationService.displayError(error);

            });
        }

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