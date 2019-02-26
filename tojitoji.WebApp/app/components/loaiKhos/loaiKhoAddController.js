(function (app) {
    app.controller('loaiKhoAddController', loaiKhoAddController);

    loaiKhoAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    function loaiKhoAddController(apiService, $scope, notificationService, $state) {
        $scope.loaiKho = {};
        $scope.AddLoaiKho = AddLoaiKho;

        function AddLoaiKho() {
            apiService.post('/api/loaikho/create', $scope.loaiKho,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('loaiKhos');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    }
})(angular.module('tojitojishop.loaiKhos'));