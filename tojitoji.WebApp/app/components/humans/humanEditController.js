(function (app) {
    app.controller('humanEditController', humanEditController);

    humanEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function humanEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.humans = {};
        $scope.flatFolders = [];
        $scope.UpdateHuman = UpdateHuman;

        function loadHumanDetail() {
            apiService.get('/api/human/getbyid/' + $stateParams.id, null, function (result) {
                $scope.humans = result.data;
                $scope.humans.DateOfBirth = new Date(result.data.DateOfBirth);
                $scope.humans.DateOfEntry = new Date(result.data.DateOfEntry);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateHuman() {
            apiService.put('/api/human/update', $scope.humans,
                function (result) {
                    notificationService.displaySuccess(result.data.LastName + ' ' + result.data.FirstName + ' đã được cập nhật.');
                    $state.go('humans');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
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

        loadHumanDetail();
    }
})(angular.module('tojitojishop.humans'));