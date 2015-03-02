
application.factory('mp3Factory',
    ['$resource', 'urls',
    function ($resource, urls) {

        var webApiUrl = urls.webApiUrl;
        var concatUrl = function (appendingUrl) {
            return webApiUrl + appendingUrl;
        }

        console.log(concatUrl('api/MP3/:id'));

        return $resource(concatUrl('api/MP3/:id'), {}, {
            getAllSongs: { method: 'GET', isArray: true },
            getSong: { method: 'GET', params: { mp3Id: '@id'}, isArray: false},
            deleteSong: { method: 'DELETE', params: { mp3Id: '@id' } },
            createSong: { method: 'POST' },
            updateSong: { method: 'PUT', params: { mp3Id: '@id'} }
    })
}]);