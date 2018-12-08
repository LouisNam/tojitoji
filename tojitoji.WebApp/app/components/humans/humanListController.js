/// <reference path="D:\tojitoji\tojitojiShop\tojitoji.WebApp\Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('humanListController', humanListController);

    humanListController.$inject = ['$scope', 'apiService', 'ModalService', '$ngBootbox', 'notificationService'];

    function humanListController($scope, apiService, ModalService, $ngBootbox, notificationService) {
        $scope.human = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getHuman = getHuman;

        $scope.showDetail = showDetail;

        $scope.deleteHuman = deleteHuman;

        function deleteHuman(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/human/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    getHuman();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function showDetail(id) {
            ModalService.showModal({
                templateUrl: "/app/components/humans/humanDetailView.html",
                controller: "humanDetailController",
                preClose: (modal) => { modal.element.modal('hide'); },
                inputs: {
                    id: id
                }
            }).then(function (modal) {
                modal.element.modal();
                modal.close;
            }).catch(function (error) {
                console.log(error);
            });
        }

        function getHuman(page) {
            page = page || 0;
            var config = {
                params: {
                    page: page,
                    pageSize: 20
                }
            }

            $scope.loading = true;
            apiService.get('/api/human/getall', config, function (result) {
                if (result.data.TotalCount == 0) {
                }
                $scope.human = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPages;
                $scope.totalCount = result.data.TotalCount;
                $scope.loading = false;
            }, function () {
                $scope.loading = false;
                console.log('Load human failed.');
            });
        }

        $scope.getHuman();
    }
})(angular.module('tojitojishop.companyInformations'));