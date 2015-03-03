﻿
application.controller("singleMp3Controller",
    ["$scope", "$location", "$routeParams", "mp3Factory", "playlistFactory",
    function singleMp3Controller($scope, $location, $routeParams, mp3Factory, playlistFactory) {

        var isNew = false;

        mp3Factory.getSong({ mp3Id: $routeParams.mp3Id }).$promise
        .then(function(data) {
            $scope.mp3 = data;

            if ($scope.mp3 == null) isNew = true;

            console.log("this is mp3");
            console.log($scope.mp3);

            if (isNew) {
                $scope.mp3 = {};
                $scope.mp3.Playlist = [];
            }

            playlistFactory.getAllPlaylists().$promise
            .then(function (data) {
                $scope.playlists = data;
                console.log("Playlists fetched successfully!");
            })
            .catch(function (error) {
                console.log(error);
            });
        })
        .catch(function(error) {
            console.log(error);
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
                    $location.path("/");
                })
                .catch(function (error) {
                    console.log(error);
                });
            }
            else {
                mp3Factory.updateSong({ id: $scope.mp3.MP3ID }, $scope.mp3).$promise
                .then(function () {
                    console.log("Song updated successfully!");
                    $location.path("/");
                })
                .catch(function (error) {
                    console.log(error);
                });
            }
        }
    }]);
