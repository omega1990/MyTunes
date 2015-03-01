
application.factory('playlistFactory',
    ['$resource', 'urls',
    function ($resource, urls) {

        var webApiUrl = urls.webApiUrl;
        var concatUrl = function (appendingUrl) {
            return webApiUrl + appendingUrl;
        }

        console.log(concatUrl('api/playlist/:id'));

        return $resource(concatUrl('api/playlist/:id'), {}, {
            getAllPlaylists: { method: 'GET', isArray: true }
        })
    }]);