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
                    notificationService.displaySuccess(result.data.Type_1 + ' đã được thêm mới.');
                    $state.go('humanTypes');
                }, function (error) {
                    console.log(error)
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    }
})(angular.module('tojitojishop.humanTypes'));