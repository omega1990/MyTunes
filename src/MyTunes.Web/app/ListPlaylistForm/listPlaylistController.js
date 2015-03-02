

application.controller("listPlaylistController",
    ["$scope", "$rootScope", "$location", "$modal", "playlistFactory", "playlistDataService",
    function listPlaylistController($scope, $rootScope, $location, $modal, playlistFactory, playlistDataService) {

        playlistFactory.getAllPlaylists().$promise
            .then(function (data) {
                $scope.playlists = data;
                 console.log("Playlists fetched successfully!");
             })
            .catch(function (error) {
                console.log(error);
            });

        $scope.deletePlaylist = function deletePlaylist(playlistId) {
            $modal.open({
                templateUrl: 'app/ListPlaylistForm/DeleteModalWindow/modalTemplate.html',
                size: 'lg',
                controller: 'modalController',
            }).result
                .then(function () {
                    console.log("Deleting playlist with id " + playlistId);
                    playlistFactory.deletePlaylist({ id: playlistId })
                        .then(function () {
                            var index = -1;
                            for (var i = 0; i < $scope.playlists.length; i++) {
                                if ($scope.playlists[i].PlaylistID === playlistId) {
                                    index = i;
                                    break;
                                }
                            }
                            $scope.playlists.splice(index, 1);

                            console.log("Playlist deleted successfully!");
                        })
                        .catch(function (error) {
                            console.log(error);
                        });
                });
        }

        $scope.openCreatePlaylistForm = function openCreatePlaylistForm() {
            $location.path("/createPlaylist");

        }

        $scope.openEditPlaylistForm = function openEditPlaylistForm(playlistId) {
            $scope.playlistToEdit = {};

            playlistFactory.getPlaylist({ id: playlistId }).$promise
            .then(function (data) {
                playlistDataService.setPlaylist(data);
                console.log(data);
                $location.path("/editPlaylist");
            })
            .catch(function (error) {
                console.log(error);
            });
        }

        $scope.$on('search-event', function (event, searchQuery) {
            if (searchQuery != "") {
                playlistFactory.getFilteredPlaylists({ searchQuery: searchQuery }).$promise
                .then(function (data) {
                    console.log("Filtered!");
                    $scope.playlists = data;
                })
                .catch(function (error) {
                    console.log(error);
                });
            }
            else {
                playlistFactory.getAllPlaylists().$promise
                    .then(function (data) {
                        $scope.playlists = data;
                        console.log("Playlists fetched successfully!");
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        });

       
    }]);
