

application.controller("singlePlaylistController",
    ["$scope", "$rootScope", "$location", "$routeParams", "mp3Factory", "playlistFactory",
    function singlePlaylistController($scope, $rootScope, $location, $routeParams, mp3Factory, playlistFactory) {

        var isNew = true;

        var getAllSongs = function () {
            mp3Factory.getAllSongs().$promise
                    .then(function (data) {
                        $scope.mp3s = data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
        }

        if ($routeParams.playlistId != null) {

            playlistFactory.getPlaylist({ playlistId: $routeParams.playlistId }).$promise
            .then(function (data) {
                $scope.playlist = data;
                if ($scope.playlist != null) isNew = false;

                if (isNew) {
                    getAllSongs();
                    $scope.playlist = {};
                    $scope.playlist.MP3 = [];
                }

                else {
                    mp3Factory.getSongsNotInPlaylist({ playlistId: $scope.playlist.PlaylistID }).$promise
                    .then(function (data) {
                        $scope.mp3s = data;
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
                }
            })
            .catch(function (error) {
                console.log(error);
            });
        }
        else {
            $scope.playlist = {};
            $scope.playlist.MP3 = [];
            getAllSongs();
        }


        $scope.interacted = function (field) { return $scope.submitted || field.$dirty; };


        // Methods for adding and removing a song from the playlist
        $scope.addToPlaylist = function addToPlaylist(song) {

            for (var i = 0; i < $scope.playlist.MP3.length; i++) {
                if ($scope.playlist.MP3[i] === song) return;
            }

            $scope.playlist.MP3.push(song);
            console.log($scope.playlist.MP3);

            var index = $scope.mp3s.indexOf(song);
            if (index > -1) {
                $scope.mp3s.splice(index, 1);
            }
        }

        $scope.removeFromPlaylist = function removeFromPlaylist(song) {
            $scope.mp3s.push(song);

            var index = $scope.playlist.MP3.indexOf(song);
            if (index > -1) {
                $scope.playlist.MP3.splice(index, 1);
            }
        }

        // Submit form data
        $scope.submitData = function submitData() {
            $scope.submitted = true;

            if ($scope.playlistForm.$invalid)
                return;

            if (isNew) {
                playlistFactory.createPlaylist($scope.playlist).$promise
                .then(function() {
                    console.log("Playlist " + $scope.playlist.Name + " created succesfully");
                    $rootScope.success = "Playlist " + $scope.playlist.Name + " created successfully";
                    $location.path("/playlist/page/1");
                })
                .catch(function(error){
                    console.log(error);
                    $rootScope.error = "Error while creating " + $scope.playlist.Name + " playlist";
                    $location.path("/playlist/page/1");
                });
            }
            else {
                playlistFactory.updatePlaylist({ playlistId: $scope.playlist.PlaylistID }, $scope.playlist).$promise
                .then(function() {
                    console.log("Playlist " + $scope.playlist.Name + " updated succesfully");
                    $rootScope.success = "Playlist " + $scope.playlist.Name + " updated successfully";
                    $location.path("/playlist/page/1");
                })
                .catch(function(error){
                    console.log(error);
                    $rootScope.success = "Error while creating " + $scope.playlist.Name + " playlist";
                    $location.path("/playlist/page/1");
                });
            }
        }


    }]);
