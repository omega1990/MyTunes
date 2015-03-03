
var application = angular.module("application", ["ngRoute", "ngResource", "ui.bootstrap"]);
    
application.constant('urls', {
    webApiUrl: 'http://localhost:54152/',
    webUrl: 'http://locathost:54229/'
});

application.config(["$routeProvider", "$locationProvider",
    function ($routeProvider, $locationProvider) {
        $routeProvider
        .when("/mp3", {
            templateUrl: "app/ListMP3form/listMp3Template.html",
            controller: "listMp3Controller"
        })
        .when("/playlist", {
            templateUrl: "app/ListPlaylistForm/listPlaylistTemplate.html",
            controller: "listPlaylistController"
        })
        .when("/createMp3", {
            templateUrl: "app/SingleMP3Form/singleMp3Template.html",
            controller: "singleMp3Controller"
        })
        .when("/editMp3/:mp3Id", {
            templateUrl: "app/SingleMP3Form/singleMp3Template.html",
            controller: "singleMp3Controller"
        })
        .when("/createPlaylist", {
            templateUrl: "app/SinglePlaylistForm/singlePlaylistTemplate.html",
            controller: "singlePlaylistController"
        })
        .when("/editPlaylist/:playlistId", {
            templateUrl: "app/SinglePlaylistForm/singlePlaylistTemplate.html",
            controller: "singlePlaylistController"
        })

        .otherwise({
            redirectTo: "/mp3"
        });

        //$httpProvider.defaults.headers.post['Content-Type'] = 'application/json';
        //$locationProvider.html5Mode(true);
    }]);


