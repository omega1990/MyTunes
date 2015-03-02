
application.controller("singleMp3Controller",
    ["$scope", "$location", "mp3Factory", "playlistFactory", "mp3DataService",
    function singleMp3Controller($scope, $location, mp3Factory, playlistFactory, mp3DataService) {

        $scope.mp3 = {};
        $scope.mp3.Playlist = [];

        $scope.mp3 = mp3DataService.getMp3();

        var isNew = false;
        if ($scope.mp3 == null) isNew = true;

        console.log("this is mp3");
        console.log($scope.mp3);
        mp3DataService.clearMp3();

        $scope.playlists = playlistFactory.getAllPlaylists();


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
                console.log($scope.mp3);
            }
            else {
                var index = -1;
                for (var i = 0; i < $scope.mp3.Playlist.length; i++)
                {
                    console.log($scope.mp3.Playlist[i].PlaylistID);
                    console.log(playlist.PlaylistID);
                    if($scope.mp3.Playlist[i].PlaylistID == playlist.PlaylistID) 
                    {
                        index = i;
                        break;
                    }
                }

                if (index > -1) {
                    $scope.mp3.Playlist.splice(index, 1);
                }
            }
        };

        $scope.submitData = function submitData() {
            console.log($scope.mp3);

            if (isNew) {
                mp3Factory.createSong($scope.mp3).$promise
                .then(function () {
                    $location.path("/");
                });
            }
            else {
                mp3Factory.updateSong({ id: $scope.mp3.MP3ID }, $scope.mp3).$promise
                .then(function () {
                    $location.path("/");
                });
            }
        }
    }]);
