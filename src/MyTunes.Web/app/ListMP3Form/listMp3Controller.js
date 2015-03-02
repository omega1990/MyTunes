
application.controller("listMp3Controller",
    ["$scope", "$modal", "$location", "mp3Factory", "mp3DataService",
    function listMp3Controller($scope, $modal, $location, mp3Factory, mp3DataService) {

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


        $scope.openCreateMp3Form = function openCreateMp3Form() {
            $location.path("/createMp3");
        }


        $scope.openEditMp3Form = function openEditMp3Form(mp3Id) {
            $scope.mp3ToEdit = {};

            mp3Factory.getSong({ id: mp3Id }).$promise
            .then(function (data) {
                mp3DataService.setMp3(data);
                console.log(data);
                $location.path("/editMp3");
            });
        }



    }]);
