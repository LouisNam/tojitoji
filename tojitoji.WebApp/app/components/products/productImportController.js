(function (app) {
    app.controller('productImportController', productImportController);

    productImportController.$inject = ['apiService', '$http', '$scope', 'notificationService', '$state'];

    function productImportController(apiService, $http, $scope, notificationService, $state) {
        $scope.files = [];
        $scope.importProduct = importProduct;

        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                $scope.files.push(args.file);
            });
        });

        function importProduct() {
            $http({
                method: 'POST',
                url: "/api/product/import",                
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    var formData = new FormData();
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i]);
                    }
                    return formData;
                },
                data: { files: $scope.files }
            }).then(function (result, status, headers, config) {
                notificationService.displaySuccess(result.data);
                $state.go('products');
            },
            function (data, status, headers, config) {
                notificationService.displayError(data.data.Message);
            });
        }
    }
})(angular.module('tojitojishop.products'));