﻿(function (app) {
    app.controller('purchaseOrderAddController', purchaseOrderAddController);

    purchaseOrderAddController.$inject = ['$scope', 'apiService', 'notificationService', '$rootScope', '$state'];

    function purchaseOrderAddController($scope, apiService, notificationService, $rootScope, $state) {
        $scope.purchaseOrder = {};
        $scope.AddPurchaseOrder = AddPurchaseOrder;

        function AddPurchaseOrder() {
            apiService.post('/api/purchaseorder/create', $scope.purchaseOrder,
                function (result) {                    
                    notificationService.displaySuccess('Thêm thành công!');
                    $state.go('purchaseOrders');
                    location.reload();
                    $rootScope.$emit('loadDetail', result.data.ID);                  
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công!');
                });
        };

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

        loadHumans();
        loadDocumentType();
    };
})(angular.module('tojitojishop.purchaseOrders'));