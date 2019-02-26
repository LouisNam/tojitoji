(function (app) {
    app.controller('loaiTaiSanEditController', loaiTaiSanEditController);

    loaiTaiSanEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function loaiTaiSanEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.loaiTaiSan = {};
        $scope.UpdateLoaiTaiSan = UpdateLoaiTaiSan;

        function loadLoaiTaiSanDetail() {
            apiService.get('/api/loaitaisan/getbyid/' + $stateParams.id, null, function (result) {
                $scope.loaiTaiSan = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateLoaiTaiSan() {
            apiService.put('/api/loaitaisan/update', $scope.loaiTaiSan,
                function (result) {
                    notificationService.displaySuccess(result.data.Name +' đã được cập nhật.');
                    $state.go('loaiTaiSans');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }        

        loadLoaiTaiSanDetail();
    }
})(angular.module('tojitojishop.loaiTaiSans'));