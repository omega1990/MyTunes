
application.service('mp3DataService', function () {
    var mp3 = null;

    var setMp3 = function (passedMp3) {
        mp3 = passedMp3;
    }

    var getMp3 = function () {
        return mp3;
    }

    var clearMp3 = function () {
        mp3 = null
    }

    return {
        setMp3: setMp3,
        getMp3: getMp3,
        clearMp3: clearMp3
    };

});