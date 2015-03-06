
application.controller("listMp3Controller",
    ["$scope", "$rootScope", "$modal", "$location", "$route", "$routeParams", "mp3Factory", 
    function listMp3Controller($scope, $rootScope, $modal, $location, $route, $routeParams, mp3Factory) {

        $scope.isSearchActivated = false;
        $scope.searchText = "";
      
        mp3Factory.getPaginated({ page: $routeParams.page - 1}).$promise
        .then(function(data) {
            if (data.CurrentPage >= data.Pages.length) $location.path("mp3/page/" + data.Pages.length);
            $scope.mp3s = data.PagedData;
            $scope.navButtonNumber = data.Pages;
            $scope.currentPageItem = data.CurrentPage;
            $scope.nextActive = data.NextActive;
            $scope.previousActive = data.PreviousActive;
        })
        .catch(function(error) {
            console.log(error);
            $rootScope.error = "Error occured while getting songs";
        });

        $scope.routePage = function routePage(page) {
            page++;
            $location.path("mp3/page/" + page);
        };

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
                        console.log("Song deleted.");
                        $rootScope.success = "Song " + mp3Title + " deleted sucessfully";
                        $route.reload();
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
                $scope.searchText = searchQuery;
            }
            else {
                $scope.isSearchActivated = false;
                $route.reload();
            }
        });

    }]);
