(function (app) {
    app.controller('loaiTaiSanAddController', loaiTaiSanAddController);

    loaiTaiSanAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function loaiTaiSanAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.loaiTaiSan = {};
        $scope.AddLoaiTaiSan = AddLoaiTaiSan;

        function AddLoaiTaiSan() {
            apiService.post('/api/loaitaisan/create', $scope.loaiTaiSan,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('loaiTaiSans');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    }
})(angular.module('tojitojishop.loaiTaiSans'));