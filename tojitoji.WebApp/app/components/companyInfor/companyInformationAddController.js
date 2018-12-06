(function (app) {
    app.controller('companyInformationAddController', companyInformationAddController);

    companyInformationAddController.$inject = ['apiService', '$scope', 'notificationService', '$state'];

    function companyInformationAddController(apiService, $scope, notificationService, $state) {
        $scope.companyInformation = {
            MSTDate: new Date()
        };

        $scope.AddCompanyInformation = AddCompanyInformation;

        function AddCompanyInformation() {
            apiService.post('/api/companyinformation/create', $scope.companyInformation,
                function (result) {
                    notificationService.displaySuccess(result.data.CompanyName + ' đã được thêm mới.');
                    $state.go('companyInformations');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
                });
        }
    }
})(angular.module('tojitojishop.companyInformations'));