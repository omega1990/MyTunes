
application.controller("singleMp3Controller",
    ["$scope", "$rootScope", "$location", "$routeParams", "mp3Factory", "playlistFactory",
    function singleMp3Controller($scope, $rootScope, $location, $routeParams, mp3Factory, playlistFactory) {

        var isNew = true;
        $scope.mp3 = {};
        $scope.mp3.Playlist = [];

        if ($routeParams.mp3Id != null) {

            mp3Factory.getSong({ mp3Id: $routeParams.mp3Id }).$promise
            .then(function (data) {
                $scope.mp3 = data;
                if ($scope.mp3 != null) isNew = false;
            })
            .catch(function (error) {
                console.log(error);
                $rootScope.error("Error occurred while retrieving song for editing.")
            });
        }

        playlistFactory.getAllPlaylists().$promise
        .then(function (data) {
            $scope.playlists = data;
            console.log("Playlists fetched successfully!");
        })
        .catch(function (error) {
            console.log(error);
            $rootScope.error("Error while retrieving playlists.");
        });

        $scope.checkSongInPlaylist = function checkSongInPlaylist(playlist) {

            for (var i = 0; i < $scope.mp3.Playlist.length; i++) {
                if ($scope.mp3.Playlist[i].PlaylistID == playlist.PlaylistID) {
                    return true;
                }
            }
        };

        $scope.addSongToPlaylist = function addSongToPlaylist($event, playlist) {
            var checkbox = $event.target;
            if (checkbox.checked) {
                $scope.mp3.Playlist.push(playlist);
                console.log("Song added to playlist: ", playlist.Name);
            }
            else {
                var index = -1;
                for (var i = 0; i < $scope.mp3.Playlist.length; i++) {
                    console.log($scope.mp3.Playlist[i].PlaylistID);
                    console.log(playlist.PlaylistID);
                    if ($scope.mp3.Playlist[i].PlaylistID == playlist.PlaylistID) {
                        index = i;
                        break;
                    }
                }

                if (index > -1) {
                    $scope.mp3.Playlist.splice(index, 1);
                    console.log("Song removed from playlist: ", playlist.Name);
                }
            }
        };

        $scope.submitData = function submitData() {
            console.log($scope.mp3);

            if ($scope.mp3Form.$invalid)
                return;

            if (isNew) {
                mp3Factory.createSong($scope.mp3).$promise
                .then(function () {
                    console.log("Song created successfully!");
                    $rootScope.success = "Song " + $scope.mp3.Title + " created successfully";
                    $location.path("/");
                })
                .catch(function (error) {
                    console.log(error);
                    $rootScope.error = "Error creating song " + $scope.mp3.Title;
                    $location.path("/");
                });
            }
            else {
                mp3Factory.updateSong({ id: $scope.mp3.MP3ID }, $scope.mp3).$promise
                .then(function () {
                    console.log("Song updated successfully!");
                    $rootScope.success = "Song " + $scope.mp3.Title + " updated successfully";
                    $location.path("/");
                })
                .catch(function (error) {
                    console.log(error);
                    console.log("Error updating song " + $scope.mp3.Title);
                    $location.path("/");
                });
            }
        }
    }]);
