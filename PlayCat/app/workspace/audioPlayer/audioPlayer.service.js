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
        this._audio.volume = 0.01;
        this.onPlaylistLoaded = new core_2.EventEmitter();
        this.onDurationChange = new core_2.EventEmitter();
        this.onTimeUpdate = new core_2.EventEmitter();
        this.onAudioChanged = new core_2.EventEmitter();
        this.onActionChanged = new core_2.EventEmitter();
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
        this._playlistService
            .getPlaylist(null, this._skip, this._take)
            .then(function (playlistResult) {
            if (playlistResult.ok) {
                _this._playlist = playlistResult.playlist;
                _this._audioCount = playlistResult.playlist.audios.length;
                _this.selectAudio(_this._currentIndex);
                _this.onPlaylistLoaded.emit(_this._playlist);
            }
        });
    }
    //register events
    //on event duration
    AudioPlayerService.prototype.getOnDurationEmitter = function () {
        return this.onDurationChange;
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
    //playlist loaded
    AudioPlayerService.prototype.getOnPlaylistLoadedEmitter = function () {
        return this.onPlaylistLoaded;
    };
    //playlist info
    AudioPlayerService.prototype.getPlaylist = function () {
        return this._playlist;
    };
    AudioPlayerService.prototype.getCurrentAudio = function () {
        return this._currentAudio;
    };
    AudioPlayerService.prototype.getCurrentTime = function () {
        return this._currentTime;
    };
    AudioPlayerService.prototype.getDuration = function () {
        return this._duration;
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
    AudioPlayerService.prototype.setVolume = function (volume) {
        this._audio.volume = volume;
    };
    AudioPlayerService.prototype.setCurrentTime = function (currentTime) {
        this._audio.currentTime = currentTime;
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
    AudioPlayerService.prototype.selectById = function (id) {
        var audio = this._playlist.audios.find(function (a) { return a.id === id; });
        if (audio) {
            this.selectAudio(this._playlist.audios.indexOf(audio));
        }
    };
    AudioPlayerService.prototype.selectAudio = function (index) {
        if (this._playlist && this._playlist.audios[index]) {
            this._currentIndex = index;
            this._currentAudio = this._playlist.audios[index];
            this._audio.src = this._currentAudio.accessUrl;
            this.onAudioChanged.emit(this._currentAudio);
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