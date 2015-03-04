

application.controller("listPlaylistController",
    ["$scope", "$rootScope", "$location", "$modal", "playlistFactory",
    function listPlaylistController($scope, $rootScope, $location, $modal, playlistFactory) {

        $scope.isSearchActivated = false;

        // Page getting logic - START
        var calculateNumberOfPages = function (count) {
            var pageSize = 10;
            var navButtonNumber = Math.ceil(count / pageSize);

            $scope.navButtonNumber = [];

            for (var i = 0; i < navButtonNumber; i++) {
                $scope.navButtonNumber.push({});

            }
        }

        $scope.getPage = function getPage(page) {
            if ($scope.navButtonNumber.length <= page) page = $scope.navButtonNumber.length - 1;
            playlistFactory.getPaginated({ page: page }).$promise
           .then(function (data) {
               $scope.playlists = data;
               console.log("Playlists fetched successfully");
               $scope.currentPlaylistPage = page;
           })
           .catch(function (error) {
               console.log(error);
           });
        }

        var initialize = function (page) {
            page = (typeof page === "undefined") ? 0 : page;

            playlistFactory.getCount().$promise
            .then(function (data) {
                var count = data.Count;
                calculateNumberOfPages(count);
                $scope.getPage(page);

            })
            .catch(function (error) {
                console.log(error);
            });
        }

        initialize();
        // Page getting logic - END
       
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
                            initialize($scope.currentPlaylistPage);
                            console.log("Playlist deleted successfully!");
                            $rootScope.success = "Playlist " + playlistName + " deleted sucessfully";
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
                $scope.getPage(0);
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

application.filter('startFrom', function () {
    return function (input, start) {
        start = +start;
        return input.slice(start);
    }
});