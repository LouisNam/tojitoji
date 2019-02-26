(function (app) {
    app.controller('loaiKhoImportController', loaiKhoImportController);

    loaiKhoImportController.$inject = ['$http', '$scope', 'notificationService', '$state'];

    function loaiKhoImportController($http, $scope, notificationService, $state) {
        $scope.files = [];
        $scope.importLoaiKho = importLoaiKho;

        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                $scope.files.push(args.file);
            });
        });

        function importLoaiKho() {
            $http({
                method: 'POST',
                url: "/api/loaikho/import",                
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
                $state.go('loaiKhos');
            },
            function (data, status, headers, config) {
                notificationService.displayError(data.data.Message);
            });
        }
    }
})(angular.module('tojitojishop.categories'));