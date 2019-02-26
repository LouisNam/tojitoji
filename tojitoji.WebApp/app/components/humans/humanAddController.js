(function (app) {
    app.controller('humanAddController', humanAddController);

    humanAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function humanAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.humans = {};
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
            apiService.get('api/humantype/getalltype', null, function (result) {
                $scope.humanTypes = result.data;
            }, function () {
                console.log('Cannot get list human type!');
            });
        }

        loadHumanType();
    }
})(angular.module('tojitojishop.humans'));