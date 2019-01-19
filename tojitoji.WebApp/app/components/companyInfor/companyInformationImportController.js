(function (app) {
    app.controller('companyInformationImportController', companyInformationImportController);

    companyInformationImportController.$inject = ['apiService', '$http', '$scope', 'notificationService', '$state', 'commonService'];

    function companyInformationImportController(apiService, $http, $scope, notificationService, $state, commonService) {
        $scope.files = [];
        $scope.importCompanyInformations = importCompanyInformations;

        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                $scope.files.push(args.file);
            });
        });

        function importCompanyInformations() {
            $http({
                method: 'POST',
                url: "/api/companyinformation/import",
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
                $state.go('companyInformations');
            },
            function (data, status, headers, config) {
                notificationService.displayError(data);
            });
        }
    }
})(angular.module('tojitojishop.companyInformations'));