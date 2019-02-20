(function (app) {
    app.controller('categoryImportController', categoryImportController);

    categoryImportController.$inject = ['apiService', '$http', '$scope', 'notificationService', '$state', 'commonService'];

    function categoryImportController(apiService, $http, $scope, notificationService, $state, commonService) {
        $scope.files = [];
        $scope.importCategory = importCategory;

        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                $scope.files.push(args.file);
            });
        });

        function importCategory() {
            $http({
                method: 'POST',
                url: "/api/category/import",                
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
                $state.go('categories');
            },
            function (data, status, headers, config) {
                notificationService.displayError(data.data.Message);
            });
        }
    }
})(angular.module('tojitojishop.categories'));