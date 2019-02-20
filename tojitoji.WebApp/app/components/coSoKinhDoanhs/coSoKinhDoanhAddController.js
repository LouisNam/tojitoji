(function (app) {
    app.controller('coSoKinhDoanhAddController', coSoKinhDoanhAddController);

    coSoKinhDoanhAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    function coSoKinhDoanhAddController(apiService, $scope, notificationService, $state) {
        $scope.coSoKinhDoanh = {};
        $scope.addCoSoKinhDoanh = addCoSoKinhDoanh;

        function addCoSoKinhDoanh() {
            apiService.post('/api/cosokinhdoanh/create', $scope.coSoKinhDoanh,
                function (result) {
                    notificationService.displaySuccess('Thêm mới thành công');
                    $state.go('coSoKinhDoanhs');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công');
                });
        }
    }
})(angular.module('tojitojishop.coSoKinhDoanhs'));