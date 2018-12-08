(function (app) {
    app.controller('humanTypeAddController', humanTypeAddController);

    humanTypeAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function humanTypeAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.humanType = {};
        $scope.flatFolders = [];
        $scope.AddHumanType = AddHumanType;

        function AddHumanType() {
            apiService.post('/api/humantype/create', $scope.humanType,
                function (result) {
                    notificationService.displaySuccess(result.data.Type + ' đã được thêm mới.');
                    $state.go('humanTypes');
                }, function (error) {
                    console.log(error)
                    notificationService.displayError('Thêm mới không thành công.');
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
})(angular.module('tojitojishop.humanTypes'));