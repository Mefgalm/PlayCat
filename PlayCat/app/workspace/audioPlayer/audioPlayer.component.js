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
var audioPlayer_service_1 = require("./audioPlayer.service");
var AudioPlayerComponent = (function () {
    function AudioPlayerComponent(_audioPlayerService) {
        var _this = this;
        this._audioPlayerService = _audioPlayerService;
        this.isPlaylistLoaded = false;
        this.display = false;
        this.isPlaying = this._audioPlayerService.isPlaying();
        this.volume = this._audioPlayerService.getVolume() * 100;
        //this.playlists = this._audioPlayerService.getPlaylists();
        this.currentPlaylist = this._audioPlayerService.getCurrentPlaylist();
        this.audio = this._audioPlayerService.getCurrentAudio();
        this.currentTime = this._audioPlayerService.getCurrentTime();
        this.duration = this._audioPlayerService.getDuration();
        this.isLoop = this._audioPlayerService.isLoop();
        if (this.playlists) {
            this.isPlaylistLoaded = true;
        }
        else {
            this._playlistLoadedSubscription = this._audioPlayerService.getOnPlayerLoadedEmitter()
                .subscribe(function (playlists) {
                _this.playlists = playlists;
                _this.isPlaylistLoaded = true;
            });
        }
        this._audioChangedSubscription = this._audioPlayerService.getOnAudioChanged()
            .subscribe(function (audio) { return _this.audio = audio; });
        this._durationChangeSubscription = this._audioPlayerService.getOnDurationEmitter()
            .subscribe(function (duration) { return _this.duration = duration; });
        this._timeUpdateSubscription = this._audioPlayerService.getOnTimeUpdateEmitter()
            .subscribe(function (currentTime) { return _this.currentTime = currentTime; });
        this._actionChangedSubscription = this._audioPlayerService.getOnActionChanged()
            .subscribe(function (isPlaingAction) { return _this.isPlaying = isPlaingAction; });
        this._playlistUpdatedSubscription = this._audioPlayerService.getOnPlaylistUpdated()
            .subscribe(function (playlist) {
            var index = _this.playlists.findIndex(function (x) { return x.id == playlist.id; });
            if (index !== -1) {
                _this.playlists.splice(index, 1);
                _this.playlists.splice(index, 0, playlist);
            }
        });
        this._isLoopChangedSubscription = this._audioPlayerService.getOnIsLoopEmitter()
            .subscribe(function (isLoop) { return _this.isLoop = isLoop; });
        this._playlistChangedSubscription = this._audioPlayerService.getOnPlaylistChanged()
            .subscribe(function (currentPlaylist) { return _this.currentPlaylist = currentPlaylist; });
    }
    AudioPlayerComponent.prototype.showDialog = function () {
        this.display = true;
    };
    AudioPlayerComponent.prototype.hideDialog = function () {
        this.display = false;
    };
    AudioPlayerComponent.prototype.play = function () {
        this._audioPlayerService.play();
    };
    AudioPlayerComponent.prototype.playById = function (id) {
        this._audioPlayerService.playById(id);
    };
    AudioPlayerComponent.prototype.pause = function () {
        this._audioPlayerService.pause();
    };
    AudioPlayerComponent.prototype.next = function () {
        this._audioPlayerService.next();
    };
    AudioPlayerComponent.prototype.toggleLoop = function () {
        this._audioPlayerService.setIsLoop(!this.isLoop);
    };
    AudioPlayerComponent.prototype.previous = function () {
        this._audioPlayerService.previous();
    };
    AudioPlayerComponent.prototype.volumeChanged = function (value) {
        this.volume = value;
        this._audioPlayerService.setVolume(this.volume / 100);
    };
    AudioPlayerComponent.prototype.currentTimeChanged = function (value) {
        this.currentTime = value;
        this._audioPlayerService.setCurrentTime(this.currentTime);
    };
    AudioPlayerComponent.prototype.selectPlaylist = function (id) {
        this._audioPlayerService.selectPlaylist(id);
    };
    AudioPlayerComponent.prototype.createPlaylist = function (playlist) {
        this._audioPlayerService.createPlaylist(playlist.title);
    };
    AudioPlayerComponent.prototype.updatePlaylist = function (playlist) {
        this._audioPlayerService.updatePlaylist(playlist.id, playlist.title);
    };
    AudioPlayerComponent.prototype.deletePlaylist = function (playlist) {
        this._audioPlayerService.deletePlaylist(playlist.id);
    };
    AudioPlayerComponent.prototype.onNgDestroy = function () {
        this._playlistLoadedSubscription.unsubscribe();
        this._audioChangedSubscription.unsubscribe();
        this._durationChangeSubscription.unsubscribe();
        this._timeUpdateSubscription.unsubscribe();
        this._isLoopChangedSubscription.unsubscribe();
    };
    return AudioPlayerComponent;
}());
__decorate([
    core_1.ViewChild('progressBar'),
    __metadata("design:type", Object)
], AudioPlayerComponent.prototype, "progressBar", void 0);
AudioPlayerComponent = __decorate([
    core_1.Component({
        selector: 'audioPlayer',
        templateUrl: './app/workspace/audioPlayer/audioPlayer.component.html',
        styleUrls: [
            './app/workspace/audioPlayer/audioPlayer.component.css',
        ],
    }),
    __metadata("design:paramtypes", [audioPlayer_service_1.AudioPlayerService])
], AudioPlayerComponent);
exports.AudioPlayerComponent = AudioPlayerComponent;
//# sourceMappingURL=audioPlayer.component.js.map