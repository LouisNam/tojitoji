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
                    notificationService.displaySuccess(result.data.Type_1 + ' đã được cập nhật.');
                    $state.go('humanTypes');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }        

        loadHumanTypeDetail();
    }
})(angular.module('tojitojishop.humanTypes'));