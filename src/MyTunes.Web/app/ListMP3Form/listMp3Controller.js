
application.controller("listMp3Controller",
    ["$scope", "$modal", "mp3Factory",
    function listMp3Controller($scope, $modal, mp3Factory) {

        $scope.tog = 1;
        $scope.mp3s = mp3Factory.getAllSongs();
         
        console.log($scope.mp3s);


        $scope.deleteMp3 = function deleteMp3(mp3Id) {
            $modal.open({
                templateUrl: 'app/ListMP3Form/DeleteModalWindow/modalTemplate.html',
                size: 'lg',
                controller: 'modalController',
            }).result
                .then(function () {
                    console.log("Deleting song with id " + mp3Id);
                    mp3Factory.deleteSong({ id: mp3Id });

                    console.log("deleting");
                    var index = -1;
                    for (var i = 0; i < $scope.mp3s.length; i++) {
                        if ($scope.mp3s[i].MP3ID === mp3Id) {
                            index = i;
                            break;
                        }
                    }
                    $scope.mp3s.splice(index, 1);

                    console.log("Song deleted.");
                });
        }




    }]);
