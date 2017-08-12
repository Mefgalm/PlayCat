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
var AudioPlayerService = (function () {
    function AudioPlayerService() {
        this.onLoaded = new core_2.EventEmitter();
        this._currentIndex = 0;
    }
    AudioPlayerService.prototype.emitOnLoadedEvent = function (number) {
        this.onLoaded.emit(number);
    };
    AudioPlayerService.prototype.getOnLoadedEmitter = function () {
        return this.onLoaded;
    };
    AudioPlayerService.prototype.setPlaylist = function (playlist) {
        this._playlist = playlist;
        this._audioCount = playlist.audios.length;
        this.playAudio(this._currentIndex);
        this.emitOnLoadedEvent(true);
    };
    AudioPlayerService.prototype.isLoaded = function () {
        return this._playlist !== null;
    };
    AudioPlayerService.prototype.getAudios = function () {
        if (this._playlist) {
            return this._playlist.audios;
        }
    };
    AudioPlayerService.prototype.getTitle = function () {
        if (this._playlist) {
            return this._playlist.title;
        }
    };
    AudioPlayerService.prototype.playAudio = function (index) {
        if (this._playlist && this._playlist.audios[index]) {
            this._currentIndex = index;
            this._currentAudio = this._playlist.audios[index];
        }
    };
    AudioPlayerService.prototype.getArtist = function () {
        return this._currentAudio.artist;
    };
    AudioPlayerService.prototype.getSong = function () {
        return this._currentAudio.song;
    };
    AudioPlayerService.prototype.getCurrentAudio = function () {
        return this._currentAudio;
    };
    AudioPlayerService.prototype.playNext = function () {
        this.playAudio(this._currentIndex + 1);
    };
    AudioPlayerService.prototype.playPrev = function () {
        this.playAudio(this._currentIndex - 1);
    };
    return AudioPlayerService;
}());
AudioPlayerService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [])
], AudioPlayerService);
exports.AudioPlayerService = AudioPlayerService;
//# sourceMappingURL=audioPlayer.service.js.map