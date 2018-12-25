(function (app) {
    app.controller('purchaseOrderEditController', purchaseOrderEditController);

    purchaseOrderEditController.$inject = ['$scope', 'apiService', 'ModalService', 'notificationService', '$state', '$stateParams'];

    function purchaseOrderEditController($scope, apiService, ModalService, notificationService, $state, $stateParams) {
        $scope.purchaseOrder = {};
        $scope.UpdatePurchaseOrder = UpdatePurchaseOrder;

        function loadPurchaseOrderDetail() {
            apiService.get('/api/purchaseorder/getbyid/' + $stateParams.id, null, function (result) {
                $scope.purchaseOrder = result.data;
                $scope.purchaseOrder.SubmittedDate = new Date(result.data.SubmittedDate);
                $scope.purchaseOrder.ApprovedDate = new Date(result.data.ApprovedDate);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdatePurchaseOrder() {
            apiService.put('/api/purchaseorder/update', $scope.purchaseOrder,
                function (result) {
                    notificationService.displaySuccess('Chi tiết ' + result.data.ID + ' đã được cập nhật.');
                    $state.go('purchaseOrders');
                    location.reload();
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function loadDocumentType() {
            apiService.get('api/documenttype/getalldocumenttype', null, function (result) {
                $scope.documentTypes = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        function loadHumans() {
            apiService.get('api/human/getallhumans', null, function (result) {
                $scope.humans = result.data;
            }, function () {
                console.log('Cannot get list parent');
            });
        }

        loadPurchaseOrderDetail();
        loadHumans();
        loadDocumentType();
    };
})(angular.module('tojitojishop.purchaseOrders'));