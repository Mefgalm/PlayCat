"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var core_2 = require("@angular/core");
var playlist_service_1 = require("./playlist.service");
var AudioPlayerService = (function () {
    function AudioPlayerService(_playlistService) {
        var _this = this;
        this._playlistService = _playlistService;
        this._take = 50;
        this._currentIndex = 0;
        this._skip = 0;
        this._audio = new Audio();
        this._isPlaying = false;
        this._audio.volume = 0.5;
        this.onPlayerLoaded = new core_2.EventEmitter();
        this.onDurationChange = new core_2.EventEmitter();
        this.onTimeUpdate = new core_2.EventEmitter();
        this.onAudioChanged = new core_2.EventEmitter();
        this.onActionChanged = new core_2.EventEmitter();
        this.onIsLoopChanged = new core_2.EventEmitter();
        this.onPlaylistChanged = new core_2.EventEmitter();
        this._audio.onended = function () {
            _this.next();
            _this.onAudioChanged.emit(_this._currentAudio);
        };
        this._audio.ontimeupdate = function () {
            _this._currentTime = _this._audio.currentTime;
            _this.onTimeUpdate.emit(_this._audio.currentTime);
        };
        this._audio.ondurationchange = function () {
            _this._duration = _this._audio.duration;
            _this.onDurationChange.emit(_this._audio.duration);
        };
        this._audio.oncanplay = function () { return _this.onAudioChanged.emit(_this._currentAudio); };
        this._playlistService
            .userPlaylists(null, this._skip, this._take)
            .then(function (userPlaylistsResult) {
            if (userPlaylistsResult.ok) {
                _this._playlists = userPlaylistsResult.playlists;
                _this.selectPlaylist(null);
                _this.selectAudio(_this._currentIndex);
                _this.onPlayerLoaded.emit(_this._playlists);
            }
        });
    }
    //register events
    //on event duration
    AudioPlayerService.prototype.getOnDurationEmitter = function () {
        return this.onDurationChange;
    };
    AudioPlayerService.prototype.getOnIsLoopEmitter = function () {
        return this.onIsLoopChanged;
    };
    //time update
    AudioPlayerService.prototype.getOnTimeUpdateEmitter = function () {
        return this.onTimeUpdate;
    };
    AudioPlayerService.prototype.getOnActionChanged = function () {
        return this.onActionChanged;
    };
    //audio
    AudioPlayerService.prototype.getOnAudioChanged = function () {
        return this.onAudioChanged;
    };
    AudioPlayerService.prototype.getOnPlaylistChanged = function () {
        return this.onPlaylistChanged;
    };
    //playlist loaded
    AudioPlayerService.prototype.getOnPlayerLoadedEmitter = function () {
        return this.onPlayerLoaded;
    };
    //playlist info
    AudioPlayerService.prototype.getPlaylist = function () {
        return this._currentPlaylist;
    };
    AudioPlayerService.prototype.getCurrentAudio = function () {
        return this._currentAudio;
    };
    AudioPlayerService.prototype.isLoop = function () {
        return this._audio.loop;
    };
    AudioPlayerService.prototype.getCurrentTime = function () {
        return this._currentTime;
    };
    AudioPlayerService.prototype.getDuration = function () {
        return this._duration;
    };
    AudioPlayerService.prototype.getPlaylists = function () {
        return this._playlists;
    };
    AudioPlayerService.prototype.getCurrentPlaylist = function () {
        return this._currentPlaylist;
    };
    //controls
    AudioPlayerService.prototype.play = function () {
        this._audio.play();
        this._isPlaying = true;
        this.onActionChanged.emit(this._isPlaying);
    };
    AudioPlayerService.prototype.playById = function (id) {
        this.selectById(id);
        this.play();
    };
    AudioPlayerService.prototype.pause = function () {
        this._audio.pause();
        this._isPlaying = false;
        this.onActionChanged.emit(this._isPlaying);
    };
    AudioPlayerService.prototype.isPlaying = function () {
        return this._isPlaying;
    };
    AudioPlayerService.prototype.setIsLoop = function (isLoop) {
        this._audio.loop = isLoop;
        this.onIsLoopChanged.emit(this._audio.loop);
    };
    AudioPlayerService.prototype.setVolume = function (volume) {
        if (volume >= 0 && volume <= 100) {
            this._audio.volume = volume;
        }
    };
    AudioPlayerService.prototype.getVolume = function () {
        return this._audio.volume;
    };
    AudioPlayerService.prototype.setCurrentTime = function (currentTime) {
        if (currentTime >= 0 && currentTime <= this._duration) {
            this._audio.currentTime = currentTime;
        }
    };
    AudioPlayerService.prototype.next = function () {
        this.selectAudio(this._currentIndex + 1);
        if (this._isPlaying)
            this.play();
    };
    AudioPlayerService.prototype.previous = function () {
        this.selectAudio(this._currentIndex - 1);
        if (this._isPlaying)
            this.play();
    };
    AudioPlayerService.prototype.selectPlaylist = function (id) {
        if (id) {
            this._currentPlaylist = this._playlists.find(function (x) { return x.id === id; });
        }
        else {
            this._currentPlaylist = this._playlists.find(function (x) { return x.isGeneral; });
        }
        this.onPlaylistChanged.emit(this._currentPlaylist);
    };
    AudioPlayerService.prototype.selectById = function (id) {
        var audio = this._currentPlaylist.audios.find(function (a) { return a.id === id; });
        if (audio) {
            this.selectAudio(this._currentPlaylist.audios.indexOf(audio));
        }
    };
    AudioPlayerService.prototype.selectAudio = function (index) {
        if (this._currentPlaylist && this._currentPlaylist.audios[index]) {
            this._currentIndex = index;
            this._currentAudio = this._currentPlaylist.audios[index];
            this._audio.src = this._currentAudio.accessUrl;
        }
    };
    return AudioPlayerService;
}());
AudioPlayerService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [playlist_service_1.PlaylistService])
], AudioPlayerService);
exports.AudioPlayerService = AudioPlayerService;
//# sourceMappingURL=audioPlayer.service.js.map