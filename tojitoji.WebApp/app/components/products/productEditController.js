(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function productEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.product = {};
        $scope.flatFolders = [];
        $scope.UpdateProduct = UpdateProduct;
        $scope.moreImages = [];

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.MainImage = fileUrl;
                })
            }
            finder.popup();
        }        

        $scope.ChooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            }
            finder.popup();
        }

        function loadProductDetail() {
            apiService.get('/api/product/getbyid/' + $stateParams.id, null, function (result) {
                $scope.product = result.data;
                if ($scope.product.MoreImage == null) {
                    $scope.moreImages = [];                    
                } else {
                    $scope.moreImages = JSON.parse($scope.product.MoreImage);
                }
                
                $scope.product.SpecialFromTime = new Date(result.data.SpecialFromTime);
                $scope.product.SpecialToTime = new Date(result.data.SpecialToTime)
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateProduct() {
            $scope.product.MoreImage = JSON.stringify($scope.moreImages);
            apiService.put('/api/product/update', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name +' đã được cập nhật.');
                    $state.go('products');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function loadCategory() {
            apiService.get('api/category/getallparents', null, function (result) {
                $scope.categories = commonService.getTree(result.data, "ID", "ParentID");
                $scope.categories.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                });
            }, function () {
                console.log('Cannot get list product!');
            });
        }

        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };

        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.Name,
                ID: item.ID,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };

        loadCategory();
        loadProductDetail();
    }
})(angular.module('tojitojishop.products'));