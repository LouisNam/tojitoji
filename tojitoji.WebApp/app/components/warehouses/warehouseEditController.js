﻿(function (app) {
    app.controller('warehouseEditController', warehouseEditController);

    warehouseEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function warehouseEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.warehouse = {};
        $scope.UpdateWarehouse = UpdateWarehouse;
        $scope.flatFolders = [];

        function loadWarehouseDetail() {
            apiService.get('/api/warehouse/getbyid/' + $stateParams.id, null, function (result) {
                $scope.warehouse = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateWarehouse() {
            apiService.put('/api/warehouse/update', $scope.warehouse,
                function (result) {
                    notificationService.displaySuccess(result.data.Name +' đã được cập nhật.');
                    $state.go('warehouses');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function loadWarehouse() {
            apiService.get('api/warehouse/getallwarehouse', null, function (result) {
                $scope.warehouses = commonService.getTree(result.data, "ID", "ParentID");
                $scope.warehouses.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                });
            }, function () {
                console.log('Cannot get list warehouse!');
            });
        }

        function times(n, str) {
            var result = '';
            for (var i = 0; i < n; i++) {
                result += str;
            }
            return result;
        };

        function recur(item, level, arr) {
            arr.push({
                Name: times(level, '–') + ' ' + item.Name,
                ID: item.ID,
                Level: level,
                Indent: times(level, '–')
            });
            if (item.children) {
                item.children.forEach(function (item) {
                    recur(item, level + 1, arr);
                });
            }
        };

        loadWarehouse();
        loadWarehouseDetail();
    }
})(angular.module('tojitojishop.warehouses'));