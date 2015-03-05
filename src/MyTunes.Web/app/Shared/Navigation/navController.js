
application.controller("navController",
    ["$scope", "$rootScope", "$location",
    function navController($scope, $rootScope, $location) {
        $scope.getClass = function (path) {
            if ($location.path().substr(0, path.length) == path) {
                return "active"
            } else {
                return ""
            }
        }

        $scope.search = function search() {
                $rootScope.$broadcast('search-event', $scope.searchString);
        };
    }]);
