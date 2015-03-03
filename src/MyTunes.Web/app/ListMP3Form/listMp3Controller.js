
application.controller("listMp3Controller",
    ["$scope", "$modal", "$location", "mp3Factory", "mp3DataService",
    function listMp3Controller($scope, $modal, $location, mp3Factory, mp3DataService) {

        mp3Factory.getAllSongs().$promise
        .then(function (data) {
            $scope.mp3s = data;
            console.log("Songs fetched successfully!");
        })
        .catch(function (error) {
            console.log(error);
        });


        $scope.deleteMp3 = function deleteMp3(mp3Id) {
            $modal.open({
                templateUrl: 'app/ListMP3Form/DeleteModalWindow/modalTemplate.html',
                size: 'lg',
                controller: 'modalController',
            }).result
                .then(function () {
                    console.log("Deleting song with id " + mp3Id);
                    mp3Factory.deleteSong({ id: mp3Id }).$promise
                    .then(function () {
                        var index = -1;
                        for (var i = 0; i < $scope.mp3s.length; i++) {
                            if ($scope.mp3s[i].MP3ID === mp3Id) {
                                index = i;
                                break;
                            }
                        }
                        $scope.mp3s.splice(index, 1);

                        console.log("Song deleted.");
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
                });
        }

        $scope.openCreateMp3Form = function openCreateMp3Form() {
            $location.path("/createMp3");
        }

        $scope.openEditMp3Form = function openEditMp3Form(mp3Id) {
            $location.path("/editMp3/" + mp3Id);

        }


        $scope.$on('search-event', function (event, searchQuery) {
            if (searchQuery != "") {
                mp3Factory.getFilteredMp3s({ searchQuery: searchQuery }).$promise
                .then(function (data) {
                    console.log("Filtered!");
                    $scope.mp3s = data;
                })
                .catch(function (error) {
                    console.log(error);
                });
            }
            else {
                mp3Factory.getAllSongs().$promise
                    .then(function (data) {
                        $scope.mp3s = data;
                        console.log("Playlists fetched successfully!");
                    })
                    .catch(function (error) {
                        console.log(error);
                    });
            }
        });



    }]);
