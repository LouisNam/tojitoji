(function (app) {
    app.controller('taiSanImportController', taiSanImportController);

    taiSanImportController.$inject = ['apiService', '$http', '$scope', 'notificationService', '$state'];

    function taiSanImportController(apiService, $http, $scope, notificationService, $state) {
        $scope.files = [];
        $scope.loaiTaiSan;
        $scope.importTaiSan = importTaiSan;        

        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                $scope.files.push(args.file);
            });
        });

        function importTaiSan() {
            $http({
                method: 'POST',
                url: "/api/taisan/import",                
                headers: { 'Content-Type': undefined },
                data: { loaiTaiSan: $scope.loaiTaiSan, files: $scope.files },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("loaiTaiSan", angular.toJson(data.loaiTaiSan));
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i]);
                    }
                    return formData;
                }                
            }).then(function (result, status, headers, config) {
                notificationService.displaySuccess(result.data);
                $state.go('taiSans');
            },
            function (data, status, headers, config) {
                notificationService.displayError(data.data.Message);
            });
        }

        function loadLoaiTaiSan() {
            apiService.get('api/loaitaisan/getalltype', null, function (result) {
                $scope.loaiTaiSans = result.data;
            }, function () {
                console.log('Cannot get list loai tai san!');
            });
        }

        loadLoaiTaiSan();
    }
})(angular.module('tojitojishop.taiSans'));