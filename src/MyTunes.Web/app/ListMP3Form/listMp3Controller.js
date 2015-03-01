
application.controller("listMp3Controller",
    ["$scope", "mp3Factory",
    function listMp3Controller($scope, mp3Factory) {

        $scope.tog = 1;
        $scope.mp3s = mp3Factory.getAllSongs();
         
        console.log($scope.mp3s);



    }]);
