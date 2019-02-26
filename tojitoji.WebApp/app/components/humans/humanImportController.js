(function (app) {
    app.controller('humanImportController', humanImportController);

    humanImportController.$inject = ['apiService', '$http', '$scope', 'notificationService', '$state', 'commonService'];

    function humanImportController(apiService, $http, $scope, notificationService, $state, commonService) {
        $scope.files = [];
        $scope.importHuman = importHuman;
        $scope.humanType;

        $scope.$on("fileSelected", function (event, args) {
            $scope.$apply(function () {
                $scope.files.push(args.file);
            });
        });

        function importHuman() {
            $http({
                method: 'POST',
                url: "/api/human/import",
                headers: { 'Content-Type': undefined },
                transformRequest: function (data) {
                    var formData = new FormData();
                    formData.append("humanType", angular.toJson(data.humanType));
                    for (var i = 0; i < data.files.length; i++) {
                        formData.append("file" + i, data.files[i]);
                    }
                    return formData;
                },
                data: { humanType: $scope.humanType, files: $scope.files }
            }).then(function (result, status, headers, config) {
                notificationService.displaySuccess(result.data);
                $state.go('humans');
            },
            function (data, status, headers, config) {
                notificationService.displayError(data.data.Message);
            });
        }

        function loadHumanType() {
            apiService.get('api/humantype/getalltype', null, function (result) {
                $scope.humanTypes = result.data;
            }, function () {
                console.log('Cannot get list human type!');
            });
        }

        loadHumanType();
    }
})(angular.module('tojitojishop.humans'));