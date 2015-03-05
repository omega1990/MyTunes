
application.controller("listMp3Controller",
    ["$scope", "$rootScope", "$modal", "$location", "$routeParams", "mp3Factory",
    function listMp3Controller($scope, $rootScope, $modal, $location, $routeParams, mp3Factory) {

        $scope.isSearchActivated = false;
        $scope.navButtonNumber = [];

        $scope.nextActive = true;
        $scope.previousActive = true;
        $scope.currentPageItem = 0;

        // Page getting logic - START
        var calculateNumberOfPages = function (count) {
            var pageSize = 10;
            var navButtonNumber = Math.ceil(count / pageSize);

            $scope.navButtonNumber = [];
            for (var i = 0; i < navButtonNumber; i++) {
                $scope.navButtonNumber.push({});

            }
        }

        $scope.routePage = function routePage(page) {
            page++;
            $location.path("mp3/page/" + page);
        };

        $scope.getPage = function getPage(page) {
            if ($scope.navButtonNumber.length <= page) page = $scope.navButtonNumber.length - 1;
            mp3Factory.getPaginated({ page: page }).$promise
           .then(function (data) {
               $scope.mp3s = data;
               console.log("Songs fetched successfully");
               $scope.currentSongPage = page;
               $scope.currentPageItem = $scope.currentSongPage + 1;
           })
           .catch(function (error) {
               console.log(error);
           });
        }

        var initialize = function (page) {
            page = (typeof page === "undefined") ? 0 : page;

            mp3Factory.getCount().$promise
            .then(function (data) {
                var count = data.Count;
                calculateNumberOfPages(count);

                if (page <= 0) $scope.previousActive = false;
                if (page >= $scope.navButtonNumber.length -1) $scope.nextActive = false;

                $scope.getPage(page);
            })
            .catch(function (error) {
                console.log(error);
            });
        }

       
        
        initialize($routeParams.page - 1);
       
        // Page getting logic - END


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
                        var mp3Title = $scope.mp3s[index].Title;
                        $scope.mp3s.splice(index, 1);
                        initialize($scope.currentSongPage);
                        console.log("Song deleted.");
                        $rootScope.success = "Song " + mp3Title + " deleted sucessfully";
                    })
                    .catch(function (error) {
                        console.log(error);
                        $rootScope.error = "Error while deleting song";
                    });
                });
        }

        $scope.openCreateMp3Form = function openCreateMp3Form() {
            $location.path("/createMp3");
        }

        $scope.openEditMp3Form = function openEditMp3Form(mp3Id) {
            $location.path("/editMp3/" + mp3Id);

        }

        // React on the change inside the search textbox
        $scope.$on('search-event', function (event, searchQuery) {
            if (searchQuery != "") {
                $scope.isSearchActivated = true;
                mp3Factory.getFilteredMp3s({ searchQuery: searchQuery }).$promise
                .then(function (data) {
                    $scope.mp3s = data;
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


     

    }]);
