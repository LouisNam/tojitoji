(function (app) {
    app.controller('humanAddController', humanAddController);

    humanAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function humanAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.humans = {};
        $scope.flatFolders = [];
        $scope.AddHuman = AddHuman;

        function AddHuman() {
            apiService.post('/api/human/create', $scope.humans,
                function (result) {
                    notificationService.displaySuccess(result.data.LastName + ' ' + result.data.FirstName + ' đã được thêm mới.');
                    $state.go('humans');
                }, function (error) {
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
})(angular.module('tojitojishop.humans'));