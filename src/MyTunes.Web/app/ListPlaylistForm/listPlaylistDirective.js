

application.directive('mp3PageNavigation', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/ListPlaylistForm/DirectivesTemplates/mp3PagingTemplate.html'
    };
});

application.directive('playlistData', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/ListPlaylistForm/DirectivesTemplates/playlistDataTemplate.html'
    };
});

application.directive('mp3Data', function () {
    return {
        restrict: 'E',
        templateUrl: 'app/ListPlaylistForm/DirectivesTemplates/mp3DataTemplate.html'
    };
});


