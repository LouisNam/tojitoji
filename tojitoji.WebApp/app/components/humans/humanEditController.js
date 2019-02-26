(function (app) {
    app.controller('humanEditController', humanEditController);

    humanEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function humanEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.humans = {};
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
            apiService.get('api/humantype/getalltype', null, function (result) {
                $scope.humanTypes = result.data;
            }, function () {
                console.log('Cannot get list human type!');
            });
        }

        loadHumanType();
        loadHumanDetail();
    }
})(angular.module('tojitojishop.humans'));