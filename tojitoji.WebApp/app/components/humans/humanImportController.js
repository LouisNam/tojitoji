(function (app) {
    app.controller('humanImportController', humanImportController);

    humanImportController.$inject = ['apiService', '$http', '$scope', 'notificationService', '$state', 'commonService'];

    function humanImportController(apiService, $http, $scope, notificationService, $state, commonService) {
        $scope.files = [];
        $scope.flatFolders = [];
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
            apiService.get('api/humantype/getallparents', null, function (result) {
                $scope.humanTypes = commonService.getTree(result.data, "ID", "ParentID");
                $scope.humanTypes.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                });
            }, function () {
                console.log('Cannot get list human type!');
            });
        }

        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };

        function recur(item, level, arr) {
            arr.push({
                Type: times(level, '–') + ' ' + item.Type,
                ID: item.ID,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };

        loadHumanType();
    }
})(angular.module('tojitojishop.humans'));