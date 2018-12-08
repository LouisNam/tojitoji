(function (app) {
    app.controller('humanTypeListController', humanTypeListController);

    humanTypeListController.$inject = ['$scope', 'apiService', 'ModalService', '$ngBootbox', 'notificationService'];

    function humanTypeListController($scope, apiService, ModalService, $ngBootbox, notificationService) {
        $scope.humanType = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getHumanType = getHumanType;

        $scope.deleteHumanType = deleteHumanType;

        function deleteHumanType(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/humanType/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getHumanType();
                }, function () {
                    notificationService.displayError('Xóa không thành công!');
                })
            });
        }

        function getHumanType(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/humantype/getall', config, function (result) {
                $scope.humanType = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load human type failed.');
            });
        }

        $scope.getHumanType();
    }
})(angular.module('tojitojishop.humanTypes'));