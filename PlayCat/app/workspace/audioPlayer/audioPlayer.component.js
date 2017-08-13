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
        this.playlist = this._audioPlayerService.getPlaylist();
        this.audio = this._audioPlayerService.getCurrentAudio();
        this.currentTime = this._audioPlayerService.getCurrentTime();
        this.duration = this._audioPlayerService.getDuration();
        if (this.playlist) {
            this.isPlaylistLoaded = true;
        }
        else {
            this._playlistLoadedSubscription = this._audioPlayerService.getOnPlaylistLoadedEmitter()
                .subscribe(function (playlist) {
                _this.playlist = playlist;
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
    AudioPlayerComponent.prototype.previous = function () {
        this._audioPlayerService.previous();
    };
    AudioPlayerComponent.prototype.onNgDestroy = function () {
        this._playlistLoadedSubscription.unsubscribe();
        this._audioChangedSubscription.unsubscribe();
        this._durationChangeSubscription.unsubscribe();
        this._timeUpdateSubscription.unsubscribe();
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