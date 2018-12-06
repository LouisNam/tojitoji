/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('companyInformationDetailController', companyInformationDetailController);

    companyInformationDetailController.$inject = ['$scope', 'apiService', 'close', 'id'];

    function companyInformationDetailController($scope, apiService, close, id) {
        $scope.companyInfors = [];

        $scope.close = function (result) {
            close(result, 500); // close, but give 500ms for bootstrap to animate
        };

        function getCompanyInforDetail(id) {
            apiService.get('/api/companyinformation/getbyid/' + id, null, function (result) {
                $scope.companyInfors = result.data;
            }, function (error) {
                //notificationService.displayError(error.data);
                console.log(error);
            });
        }

        getCompanyInforDetail(id);
    };
})(angular.module('tojitojishop.accounts'));