(function (app) {
    app.controller('bibleAddController', bibleAddController);

    bibleAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    function bibleAddController(apiService, $scope, notificationService, $state) {
        $scope.bible = {};
        $scope.addBible = addBible;

        function addBible() {
            apiService.post('/api/bible/create', $scope.bible,
                function (result) {
                    notificationService.displaySuccess(result.data.Shortcut + ' đã được thêm mới.');
                    $state.go('bibles');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    }
})(angular.module('tojitojishop.bibles'));