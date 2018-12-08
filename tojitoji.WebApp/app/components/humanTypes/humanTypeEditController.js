(function (app) {
    app.controller('humanTypeEditController', humanTypeEditController);

    humanTypeEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function humanTypeEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.humanType = {};
        $scope.flatFolders = [];
        $scope.UpdateHumanType = UpdateHumanType;

        function loadHumanTypeDetail() {
            apiService.get('/api/humantype/getbyid/' + $stateParams.id, null, function (result) {
                $scope.humanType = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateHumanType() {
            apiService.put('/api/humantype/update', $scope.humanType,
                function (result) {
                    notificationService.displaySuccess(result.data.Type + ' đã được cập nhật.');
                    $state.go('humanTypes');
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

        loadHumanTypeDetail();
        loadHumanType();
    }
})(angular.module('tojitojishop.humanTypes'));