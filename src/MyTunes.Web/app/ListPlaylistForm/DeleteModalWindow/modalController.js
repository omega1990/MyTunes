
application.controller("modalController", ["$scope", "$modalInstance",
    function modalController($scope, $modalInstance) {
        $scope.ok = function () {
            $modalInstance.close();
        };

        $scope.cancel = function () {
            $modalInstance.dismiss('cancel');
        };
    }]);