﻿(function (app) {
    app.controller('warehouseAddController', warehouseAddController);

    warehouseAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function warehouseAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.warehouse = {};
        $scope.AddWarehouse = AddWarehouse;
        $scope.flatFolders = [];

        function AddWarehouse() {
            apiService.post('/api/warehouse/create', $scope.warehouse,
                function (result) {
                    notificationService.displaySuccess(result.data.ID + ' đã được thêm mới.');
                    $state.go('warehouses');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
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
    }
})(angular.module('tojitojishop.warehouses'));