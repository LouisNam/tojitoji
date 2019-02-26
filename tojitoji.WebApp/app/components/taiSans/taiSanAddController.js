(function (app) {
    app.controller('taiSanAddController', taiSanAddController);

    taiSanAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    function taiSanAddController(apiService, $scope, notificationService, $state) {
        $scope.taiSan = {};
        $scope.AddTaiSan = AddTaiSan;

        function AddTaiSan() {
            apiService.post('/api/taisan/create', $scope.taiSan,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('taiSans');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }

        function loadLoaiTaiSan() {
            apiService.get('api/loaitaisan/getalltype', null, function (result) {
                $scope.loaiTaiSans = result.data;
            }, function () {
                console.log('Cannot get list tai san!');
            });
        }

        loadLoaiTaiSan();
    }
})(angular.module('tojitojishop.taiSans'));