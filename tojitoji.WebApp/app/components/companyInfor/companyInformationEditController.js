(function (app) {
    app.controller('companyInformationEditController', companyInformationEditController);

    companyInformationEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function companyInformationEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.companyInformation = {};

        $scope.UpdateCompanyInformation = UpdateCompanyInformation;        

        function loadCompanyInformationDetail() {
            apiService.get('/api/companyinformation/getbyid/' + $stateParams.id, null, function (result) {
                $scope.companyInformation = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateCompanyInformation() {
            apiService.put('/api/companyinformation/update', $scope.companyInformation,
                function (result) {
                    notificationService.displaySuccess(result.data.CompanyName + ' đã được cập nhật.');
                    $state.go('companyInformations');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        loadCompanyInformationDetail();
    }
})(angular.module('tojitojishop.companyInformations'));