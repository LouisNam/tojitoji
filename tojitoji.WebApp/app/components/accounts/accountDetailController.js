/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('accountDetailController', accountDetailController);

    accountDetailController.$inject = ['$scope', 'apiService', 'close', 'id'];

    function accountDetailController($scope, apiService, close, id) {
        $scope.account = [];

        $scope.close = function (result) {
            close(result, 500); // close, but give 500ms for bootstrap to animate
        };

        function getAccountDetail(id) {
            apiService.get('/api/account/getbyid/' + id, null, function (result) {
                $scope.account = result.data;
            }, function (error) {
                //notificationService.displayError(error.data);
                console.log(error);
            });
        }

        getAccountDetail(id);
    };
})(angular.module('tojitojishop.accounts'));