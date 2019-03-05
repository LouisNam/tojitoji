(function (app) {
    app.controller('skuImportController', skuImportController);

    skuImportController.$inject = ['$http', '$scope', 'notificationService', '$state'];

    function skuImportController($http, $scope, notificationService, $state) {
        $scope.files = [];
        $scope.importsku = importsku;

        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                $scope.files.push(args.file);
            });
        });

        function importsku() {
            $http({
                method: 'POST',
                url: "/api/sku/import",
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
                $state.go('skus');
            },
            function (data, status, headers, config) {
                notificationService.displayError(data.data.Message);
            });
        }
    }
})(angular.module('tojitojishop.skus'));