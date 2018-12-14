(function (app) {
    app.controller('inventorytransactionEditController', inventorytransactionEditController);

    inventorytransactionEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function inventorytransactionEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.inventorytransaction = {};
        $scope.UpdateInventoryTransaction = UpdateInventoryTransaction;
        $scope.flatFolders = [];

        function loadInventoryTransactionDetail() {
            apiService.get('/api/inventorytransaction/getbyid/' + $stateParams.id, null, function (result) {
                $scope.inventorytransaction = result.data;
                $scope.inventorytransaction.ModifiedDate = new Date(result.data.ModifiedDate);
                $scope.inventorytransaction.CreatedDate = new Date(result.data.CreatedDate);
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateInventoryTransaction() {
            apiService.put('/api/inventorytransaction/update', $scope.inventorytransaction,
                function (result) {
                    notificationService.displaySuccess(result.data.ID +' đã được cập nhật.');
                    $state.go('inventorytransactions');
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
        loadInventoryTransactionDetail();
    }
})(angular.module('tojitojishop.inventorytransactions'));