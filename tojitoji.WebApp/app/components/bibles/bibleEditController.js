(function (app) {
    app.controller('bibleEditController', bibleEditController);

    bibleEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function bibleEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.bible = {};        
        $scope.updateBible = updateBible;

        function loadBibleDetail() {
            apiService.get('/api/bible/getbyid/' + $stateParams.id, null, function (result) {
                $scope.bible = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function updateBible() {
            apiService.put('/api/bible/update', $scope.bible,
                function (result) {
                    notificationService.displaySuccess(result.data.Shorcut +' đã được cập nhật.');
                    $state.go('bibles');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        loadBibleDetail();
    }
})(angular.module('tojitojishop.bibles'));