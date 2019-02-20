(function (app) {
    app.controller('coSoKinhDoanhEditController', coSoKinhDoanhEditController);

    coSoKinhDoanhEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function coSoKinhDoanhEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.coSoKinhDoanh = {};        
        $scope.updateCoSoKinhDoanh = updateCoSoKinhDoanh;

        function loadCoSoKinhDoanhDetail() {
            apiService.get('/api/cosokinhdoanh/getbyid/' + $stateParams.id, null, function (result) {
                $scope.coSoKinhDoanh = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function updateCoSoKinhDoanh() {
            apiService.put('/api/cosokinhdoanh/update', $scope.coSoKinhDoanh,
                function (result) {
                    notificationService.displaySuccess(result.data.Shorcut +' đã được cập nhật.');
                    $state.go('coSoKinhDoanhs');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        loadCoSoKinhDoanhDetail();
    }
})(angular.module('tojitojishop.coSoKinhDoanhs'));