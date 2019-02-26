(function (app) {
    app.controller('loaiKhoEditController', loaiKhoEditController);

    loaiKhoEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function loaiKhoEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.loaiKho = {};
        $scope.EditLoaiKho = EditLoaiKho;

        function loadLoaiKhoDetail() {
            apiService.get('/api/loaikho/getbyid/' + $stateParams.id, null, function (result) {
                $scope.loaiKho = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function EditLoaiKho() {
            apiService.put('/api/loaikho/update', $scope.loaiKho,
                function (result) {
                    notificationService.displaySuccess(result.data.Name +' đã được cập nhật.');
                    $state.go('loaiKhos');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }        

        loadLoaiKhoDetail();
    }
})(angular.module('tojitojishop.loaiKhos'));