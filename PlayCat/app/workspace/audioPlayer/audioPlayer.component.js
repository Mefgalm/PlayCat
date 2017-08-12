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
var platform_browser_1 = require("@angular/platform-browser");
var AudioPlayerComponent = (function () {
    function AudioPlayerComponent(audioPlayerService, _sanitizer) {
        var _this = this;
        this.audioPlayerService = audioPlayerService;
        this._sanitizer = _sanitizer;
        this.isLoaded = false;
        this.isPlaying = false;
        this.currentTime = 0;
        this._audio = new Audio();
        this.onPlaylistLoadedSubscription = audioPlayerService.getOnLoadedEmitter()
            .subscribe(function (isLoaded) {
            _this.isLoaded = isLoaded;
            _this.selectSource();
            _this.registerEvents();
        });
    }
    AudioPlayerComponent.prototype.registerEvents = function () {
        var _this = this;
        this._audio.onended = function () { return _this.next(); };
        this._audio.ondurationchange = function () { return _this.duration = _this._audio.duration; };
        this._audio.ontimeupdate = function () { return _this.currentTime = _this._audio.currentTime; };
    };
    AudioPlayerComponent.prototype.ngOnDestroy = function () {
        this.onPlaylistLoadedSubscription.unsubscribe();
    };
    AudioPlayerComponent.prototype.selectSource = function () {
        this._audio.src = this.audioPlayerService.getCurrentAudio().accessUrl;
    };
    AudioPlayerComponent.prototype.next = function () {
        this.audioPlayerService.playNext();
        this.selectSource();
        if (this.isPlaying) {
            this.play();
        }
    };
    AudioPlayerComponent.prototype.prev = function () {
        this.audioPlayerService.playPrev();
        this.selectSource();
        if (this.isPlaying) {
            this.play();
        }
    };
    AudioPlayerComponent.prototype.pause = function () {
        this._audio.pause();
        this.isPlaying = false;
    };
    AudioPlayerComponent.prototype.play = function () {
        this._audio.play();
        this.isPlaying = true;
    };
    AudioPlayerComponent.prototype.setCurrentTime = function (value) {
        this.pause();
        this._audio.currentTime = 1 * value;
        this.play();
    };
    AudioPlayerComponent.prototype.setVolume = function (value) {
        this._audio.volume = value / 100;
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
        styleUrls: ['./app/workspace/audioPlayer/audioPlayer.component.css'],
    }),
    __metadata("design:paramtypes", [audioPlayer_service_1.AudioPlayerService, platform_browser_1.DomSanitizer])
], AudioPlayerComponent);
exports.AudioPlayerComponent = AudioPlayerComponent;
//# sourceMappingURL=audioPlayer.component.js.map