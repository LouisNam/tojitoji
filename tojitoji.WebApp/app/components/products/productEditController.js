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
                if ($scope.product.MoreImage == null || $scope.product.MoreImage == "") {
                    $scope.moreImages = [];                    
                } else {
                    $scope.moreImages = JSON.parse($scope.product.MoreImage);
                }
                
                if ($scope.product.SpecialFromTime != null) {
                    $scope.product.SpecialFromTime = new Date(result.data.SpecialFromTime);
                }
                
                if ($scope.product.SpecialToTime != null) {
                    $scope.product.SpecialToTime = new Date(result.data.SpecialToTime);
                }
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
            apiService.get('api/category/getalltype', null, function (result) {
                $scope.categories = result.data;
            }, function () {
                console.log('Cannot get list product!');
            });
        }

        loadCategory();
        loadProductDetail();
    }
})(angular.module('tojitojishop.products'));