

application.controller("playlistController",
    ["$scope", "playlistFactory",
    function playlistController($scope, playlistFactory) {

        $scope.playlists = playlistFactory.getAllPlaylists();

        console.log($scope.playlists);
    }]);
