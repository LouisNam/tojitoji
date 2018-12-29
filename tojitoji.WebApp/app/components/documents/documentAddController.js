(function (app) {
    app.controller('documentAddController', documentAddController);

    documentAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function documentAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.document = {};
        $scope.AddDocument = AddDocument;

        function AddDocument() {
            apiService.post('/api/document/create', $scope.document,
                function (result) {
                    notificationService.displaySuccess(result.data.ID + ' đã được thêm mới.');
                    $state.go('documents');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
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
    }
})(angular.module('tojitojishop.documents'));