﻿

application.controller("singlePlaylistController",
    ["$scope", "$location", "mp3Factory", "playlistDataService", "playlistFactory",
    function singlePlaylistController($scope, $location, mp3Factory, playlistDataService, playlistFactory) {

        $scope.playlist = playlistDataService.getPlaylist();

        var isNew = true;
        if ($scope.playlist != null) isNew = false;

        playlistDataService.clearPlaylist();

        if (isNew) {
            mp3Factory.getAllSongs().$promise
            .then(function (data) {
                $scope.mp3s = data;
                console.log(data);
            });

            $scope.playlist = {};
            $scope.playlist.MP3 = [];
        }

        else {
            mp3Factory.getSongsNotInPlaylist({ playlistId: $scope.playlist.PlaylistID }).$promise
            .then(function (data) {
                $scope.mp3s = data;
            });
        }
        
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
            if ($scope.playlistForm.$invalid)
                return;

            if (isNew) {
                playlistFactory.createPlaylist($scope.playlist).$promise
                .then(function () {
                    $location.path("/playlist");
                });
            }
            else {
                playlistFactory.updatePlaylist({ playlistId: $scope.playlist.PlaylistID}, $scope.playlist).$promise
                .then(function () {
                    $location.path("/playlist");
                });
            }
        }


    }]);