

application.controller("listPlaylistController",
    ["$scope", "$rootScope", "$location", "$modal", "playlistFactory",
    function listPlaylistController($scope, $rootScope, $location, $modal, playlistFactory) {


        var pageSize = 10;
        var navButtonNumber = 0;
        $scope.navButtonNumber = [];

        playlistFactory.getCount().$promise
        .then(function (data) {
            var count = data.Count;
            navButtonNumber = Math.ceil(count / pageSize);

            for (var i = 0; i < navButtonNumber; i++) {
                $scope.navButtonNumber.push({});

            }

            $scope.getPage(0);

        })
        .catch(function (error) {
            console.log(error);
        });

        $scope.getPage = function getPage(page) {
            playlistFactory.getPaginated({ page: page }).$promise
           .then(function (data) {
               $scope.playlists = data;
               console.log("Playlists fetched successfully");
           })
           .catch(function (error) {
               console.log(error);
           });
        }





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
                    playlistFactory.deletePlaylist({ id: playlistId }).$promise
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
            $location.path("/editPlaylist/" + playlistId);
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

        // Show paginated songs inside playlist
        $scope.showSongs = [];
        $scope.currentPage = [];
        $scope.pageSize = [];
        $scope.numberOfPages = [];
        $scope.pageSize = 10;

        $scope.playlistClicked = function playlistClicked(index) {
            if ($scope.showSongs[index]) {
                console.log("setting to invisible");
                $scope.showSongs[index] = false;
            }
            else {
                if ($scope.currentPage[index] != 0) $scope.currentPage[index] = 0;
                console.log($scope.currentPage[index]);
                $scope.numberOfPages[index] = function () {
                    return Math.ceil($scope.playlists[index].MP3.length / $scope.pageSize);
                }

                $scope.showSongs[index] = true;
            }
        };

    }]);

application.filter('startFrom', function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    }
});