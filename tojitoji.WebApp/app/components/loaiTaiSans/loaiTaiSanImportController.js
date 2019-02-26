(function (app) {
    app.controller('loaiTaiSanImportController', loaiTaiSanImportController);

    loaiTaiSanImportController.$inject = ['apiService', '$http', '$scope', 'notificationService', '$state'];

    function loaiTaiSanImportController(apiService, $http, $scope, notificationService, $state) {
        $scope.files = [];
        $scope.importLoaiTaiSan = importLoaiTaiSan;

        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                $scope.files.push(args.file);
            });
        });

        function importLoaiTaiSan() {
            $http({
                method: 'POST',
                url: "/api/loaitaisan/import",                
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
                $state.go('loaiTaiSans');
            },
            function (data, status, headers, config) {
                notificationService.displayError(data.data.Message);
            });
        }
    }
})(angular.module('tojitojishop.loaiTaiSans'));