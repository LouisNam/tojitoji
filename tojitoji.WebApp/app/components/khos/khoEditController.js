(function (app) {
    app.controller('khoEditController', khoEditController);

    khoEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function khoEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.kho = {};
        $scope.EditKho = EditKho;

        function loadKhoDetail() {
            apiService.get('/api/kho/getbyid/' + $stateParams.id, null, function (result) {
                $scope.kho = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function EditKho() {
            apiService.put('/api/kho/update', $scope.kho,
                function (result) {
                    notificationService.displaySuccess(result.data.Kho_1 +' đã được cập nhật.');
                    $state.go('khos');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }        

        loadKhoDetail();
    }
})(angular.module('tojitojishop.khos'));