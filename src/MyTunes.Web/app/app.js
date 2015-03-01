
var application = angular.module("application", ["ngRoute", "ngResource"]);
    
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
            templateUrl: "app/ListPlaylistForm/playlistTemplate.html",
            controller: "playlistController"
        })
        .otherwise({
            redirectTo: "/mp3"
        });

        //$httpProvider.defaults.headers.post['Content-Type'] = 'application/json';
        //$locationProvider.html5Mode(true);
    }]);

