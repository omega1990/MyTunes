
application.factory('playlistFactory',
    ['$resource', 'urls',
    function ($resource, urls) {

        var webApiUrl = urls.webApiUrl;
        var concatUrl = function (appendingUrl) {
            return webApiUrl + appendingUrl;
        }

        console.log(concatUrl('api/playlist/:id'));

        return $resource(concatUrl('api/playlist/:id'), {}, {
            getAllPlaylists: { method: 'GET', isArray: true }, 
            deletePlaylist: { method: 'DELETE', params: { playlistId: '@id' } },
            getPlaylist: { method: 'GET', params: { playlistId: '@id' }, isArray: false },
            createPlaylist: { method: 'POST' },
            updatePlaylist: { method: 'PUT', params: { playlistId: '@id' }},
        })
    }]);