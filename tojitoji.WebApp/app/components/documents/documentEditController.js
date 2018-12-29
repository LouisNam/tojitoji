(function (app) {
    app.controller('documentEditController', documentEditController);

    documentEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams'];

    function documentEditController(apiService, $scope, notificationService, $state, $stateParams) {
        $scope.document = {};
        $scope.UpdateDocument = UpdateDocument;

        function loadDocumentDetail() {
            apiService.get('/api/document/getbyid/' + $stateParams.id, null, function (result) {
                $scope.document = result.data;
                if ($scope.document.Date != null) {
                    $scope.document.Date = new Date(result.data.Date);
                }
                if ($scope.document.BillDate != null) {
                    $scope.document.BillDate = new Date(result.data.BillDate);
                }
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateDocument() {
            apiService.put('/api/document/update', $scope.document,
                function (result) {
                    notificationService.displaySuccess(result.data.Name +' đã được cập nhật.');
                    $state.go('documents');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function loadDocumentType() {
            apiService.get('api/documenttype/getalldocumenttype', null, function (result) {
                $scope.documenttypes = result.data;
            }, function () {
                console.log('Cannot get list document!');
            });
        }

        function loadHumans() {
            apiService.get('api/human/getallhumans', null, function (result) {
                $scope.humans = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadHumans();
        loadDocumentType();
        loadDocumentDetail();
    }
})(angular.module('tojitojishop.documents'));