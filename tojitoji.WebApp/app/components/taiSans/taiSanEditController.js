(function (app) {
    app.controller('taiSanEditController', taiSanEditController);

    taiSanEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function taiSanEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.taiSan = {};
        $scope.UpdateTaiSan = UpdateTaiSan;

        function loadTaiSanDetail() {
            apiService.get('/api/taisan/getbyid/' + $stateParams.id, null, function (result) {
                $scope.taiSan = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateTaiSan() {
            apiService.put('/api/taisan/update', $scope.taiSan,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được cập nhật.');
                    $state.go('taiSans');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
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
        loadTaiSanDetail();
    }
})(angular.module('tojitojishop.taiSans'));