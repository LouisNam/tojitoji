(function (app) {
    app.controller('humanTypeImportController', humanTypeImportController);

    humanTypeImportController.$inject = ['apiService', '$http', '$scope', 'notificationService', '$state', 'commonService'];

    function humanTypeImportController(apiService, $http, $scope, notificationService, $state, commonService) {
        $scope.files = [];
        $scope.importHumanType = importHumanType;

        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                $scope.files.push(args.file);
            });
        });

        function importHumanType() {
            $http({
                method: 'POST',
                url: "/api/humantype/import",
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
                $state.go('humanTypes');
            },
            function (data, status, headers, config) {
                notificationService.displayError(data.data.Message);
            });
        }
    }
})(angular.module('tojitojishop.humanTypes'));