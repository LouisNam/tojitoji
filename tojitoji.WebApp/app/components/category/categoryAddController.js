(function (app) {
    app.controller('categoryAddController', categoryAddController);

    categoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state', 'commonService'];

    function categoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.category = {};

        $scope.flatFolders = [];
        $scope.AddCategory = AddCategory;

        function AddCategory() {
            apiService.post('/api/category/create', $scope.category,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' đã được thêm mới.');
                    $state.go('categories');
                }, function (error) {
                    notificationService.displayError('Thêm mới không thành công.');
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
    }
})(angular.module('tojitojishop.categories'));