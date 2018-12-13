/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('categoryDetailController', categoryDetailController);

    categoryDetailController.$inject = ['$scope', 'apiService', 'close', 'id'];

    function categoryDetailController($scope, apiService, close, id) {
        $scope.category = [];

        $scope.close = function (result) {
            close(result, 500); // close, but give 500ms for bootstrap to animate
        };

        function getCategoryDetail(id) {
            apiService.get('/api/category/getbyid/' + id, null, function (result) {
                $scope.category = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        getCategoryDetail(id);
    };
})(angular.module('tojitojishop.categories'));