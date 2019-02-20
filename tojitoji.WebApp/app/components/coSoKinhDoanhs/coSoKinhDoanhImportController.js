(function (app) {
    app.controller('coSoKinhDoanhImportController', coSoKinhDoanhImportController);

    coSoKinhDoanhImportController.$inject = ['apiService', '$http', '$scope', 'notificationService', '$state'];

    function coSoKinhDoanhImportController(apiService, $http, $scope, notificationService, $state) {
        $scope.files = [];
        $scope.importCoSoKinhDoanh = importCoSoKinhDoanh;

        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                $scope.files.push(args.file);
            });
        });

        function importCoSoKinhDoanh() {
            $http({
                method: 'POST',
                url: "/api/cosokinhdoanh/import",
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
                $state.go('coSoKinhDoanhs');
            },
            function (data, status, headers, config) {
                notificationService.displayError(data.data.Message);
            });
        }
    }
})(angular.module('tojitojishop.coSoKinhDoanhs'));