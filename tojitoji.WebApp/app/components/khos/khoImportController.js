(function (app) {
    app.controller('khoImportController', khoImportController);

    khoImportController.$inject = ['$http', '$scope', 'notificationService', '$state', 'commonService'];

    function khoImportController($http, $scope, notificationService, $state, commonService) {
        $scope.files = [];
        $scope.importKho = importKho;

        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                $scope.files.push(args.file);
            });
        });

        function importKho() {
            $http({
                method: 'POST',
                url: "/api/kho/import",                
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
                $state.go('khos');
            },
            function (data, status, headers, config) {
                notificationService.displayError(data.data.Message);
            });
        }
    }
})(angular.module('tojitojishop.khos'));