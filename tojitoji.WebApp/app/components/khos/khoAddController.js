(function (app) {
    app.controller('khoAddController', khoAddController);

    khoAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    function khoAddController(apiService, $scope, notificationService, $state) {
        $scope.kho = {};
        $scope.AddKho = AddKho;

        function AddKho() {
            apiService.post('/api/kho/create', $scope.kho,
                function (result) {
                    notificationService.displaySuccess(result.data.Kho_1 + ' đã được thêm mới.');
                    $state.go('khos');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    }
})(angular.module('tojitojishop.khos'));