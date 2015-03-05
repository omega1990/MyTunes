

application.controller("listPlaylistController",
    ["$scope", "$rootScope", "$location", "$route", "$routeParams", "$modal", "playlistFactory",
    function listPlaylistController($scope, $rootScope, $location, $route, $routeParams, $modal, playlistFactory) {

        $scope.isSearchActivated = false;

        playlistFactory.getPaginated({ page: $routeParams.page - 1 }).$promise
        .then(function (data) {
            if (data.CurrentPage >= data.Pages.length) $location.path("playlist/page/" + data.Pages.length);
            $scope.playlists = data.PagedData;
            $scope.navButtonNumber = data.Pages;
            $scope.currentPageItem = data.CurrentPage;
            console.log($scope.currentPageItem);
            $scope.nextActive = data.NextActive;
            $scope.previousActive = data.PreviousActive;
        })
        .catch(function (error) {
            console.log(error);
            $rootScope.error = "Error occured while getting playlists";
        });

        $scope.routePage = function routePage(page) {
            page++;
            $location.path("playlist/page/" + page);
        };

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

                            var playlistName = $scope.playlists[index].Name;
                            $scope.playlists.splice(index, 1);
                            console.log("Playlist deleted successfully!");
                            $rootScope.success = "Playlist " + playlistName + " deleted sucessfully";
                            $route.reload();
                        })
                        .catch(function (error) {
                            console.log(error);
                            $rootScope.error = "Error while deleting " + playlistName + " playlist";
                        });
                });
        }

        $scope.openCreatePlaylistForm = function openCreatePlaylistForm() {
            $location.path("/createPlaylist");

        }

        $scope.openEditPlaylistForm = function openEditPlaylistForm(playlistId) {
            $location.path("/editPlaylist/" + playlistId);
        }

        // React on the change inside the search textbox
        $scope.$on('search-event', function (event, searchQuery) {
            if (searchQuery != "") {
                $scope.isSearchActivated = true;
                playlistFactory.getFilteredPlaylists({ searchQuery: searchQuery }).$promise
                .then(function (data) {
                    $scope.playlists = data;
                })
                .catch(function (error) {
                    console.log(error);
                });
            }
            else {
                $scope.isSearchActivated = false;
                $route.reload();
            }
        });

        // Show songs paginated inside the playlist
        $scope.showSongs = [];
        $scope.currentPage = [];
        $scope.pageSize = [];
        $scope.numberOfPages = [];
        $scope.pageSize = 10;

        $scope.playlistClicked = function playlistClicked(index) {
            if ($scope.showSongs[index]) {
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

