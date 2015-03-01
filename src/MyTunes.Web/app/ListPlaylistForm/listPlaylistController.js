

application.controller("listPlaylistController",
    ["$scope", "$modal", "playlistFactory",
    function listPlaylistController($scope, $modal, playlistFactory) {
        $scope.tog = 2;
         
        $scope.playlists = playlistFactory.getAllPlaylists();

        console.log($scope.playlists);


        $scope.deletePlaylist = function deletePlaylist(playlistId) {
            $modal.open({
                templateUrl: 'app/ListPlaylistForm/DeleteModalWindow/modalTemplate.html',
                size: 'lg',
                controller: 'modalController',
            }).result
                .then(function () {
                    console.log("Deleting playlist with id " + playlistId);
                    playlistFactory.deletePlaylist({ id: playlistId });

                    console.log("deleting");
                    var index = -1;
                    for (var i = 0; i < $scope.playlists.length; i++) {
                        if ($scope.playlists[i].PlaylistID === playlistId) {
                            index = i;
                            break;
                        }
                    }
                    $scope.playlists.splice(index, 1);

                    console.log("Playlist deleted.");
                });
        }


    }]);
