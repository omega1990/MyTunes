
application.service('playlistDataService', function () {
    var playlist = null;

    var setPlaylist = function (passedPlaylist) {
        playlist = passedPlaylist;
    }

    var getPlaylist = function () {
        return playlist;
    }

    var clearPlaylist = function () {
        playlist = null
    }

    return {
        setPlaylist: setPlaylist,
        getPlaylist: getPlaylist,
        clearPlaylist: clearPlaylist
    };

});