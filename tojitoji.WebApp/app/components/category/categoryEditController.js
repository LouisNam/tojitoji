(function (app) {
    app.controller('categoryEditController', categoryEditController);

    categoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];

    function categoryEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
        $scope.category = {};
        $scope.flatFolders = [];
        $scope.UpdateCategory = UpdateCategory;

        function loadCategoryDetail() {
            apiService.get('/api/category/getbyid/' + $stateParams.id, null, function (result) {
                $scope.category = result.data;
            }, function (error) {
                notificationService.displayError(error.data);
            });
        }

        function UpdateCategory() {
            apiService.put('/api/category/update', $scope.category,
                function (result) {
                    notificationService.displaySuccess(result.data.Name +' đã được cập nhật.');
                    $state.go('categories');
                }, function (error) {
                    notificationService.displayError('Cập nhật không thành công.');
                });
        }

        function loadCategory() {
            apiService.get('api/category/getallparents', null, function (result) {
                $scope.categories = commonService.getTree(result.data, "ID", "ParentID");
                $scope.categories.forEach(function (item) {
                    recur(item, 0, $scope.flatFolders);
                });
            }, function () {
                console.log('Cannot get list category!');
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

        loadCategory();

        loadCategoryDetail();
    }
})(angular.module('tojitojishop.categories'));