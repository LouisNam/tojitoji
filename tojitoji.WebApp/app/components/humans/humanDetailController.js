/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('humanDetailController', humanDetailController);

    humanDetailController.$inject = ['$scope', 'apiService', 'close', 'id'];

    function humanDetailController($scope, apiService, close, id) {
        $scope.humans = [];

        $scope.close = function (result) {
            close(result, 500); // close, but give 500ms for bootstrap to animate
        };

        function getHumanDetail(id) {
            apiService.get('/api/human/getbyid/' + id, null, function (result) {
                $scope.humans = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        getHumanDetail(id);
    };
})(angular.module('tojitojishop.humans'));