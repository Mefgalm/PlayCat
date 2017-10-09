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
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : new P(function (resolve) { resolve(result.value); }).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = y[op[0] & 2 ? "return" : op[0] ? "throw" : "next"]) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [0, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
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
        this.addToPlaylistVisible = false;
        this.selectedAudioId = null;
        this.addToPlaylistError = null;
        this.isAddToPlaylistError = false;
        this.isPlaying = this._audioPlayerService.isPlaying();
        this.volume = this._audioPlayerService.getVolume() * 100;
        this.playlists = this._audioPlayerService.getPlaylists();
        this.currentPlaylist = this._audioPlayerService.getCurrentPlaylist();
        this.audio = this._audioPlayerService.getCurrentAudio();
        this.currentTime = this._audioPlayerService.getCurrentTime();
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
    AudioPlayerComponent.prototype.selectPlaylist = function (playlist) {
        this._audioPlayerService.selectPlaylist(playlist.id);
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
    AudioPlayerComponent.prototype.onScrollDown = function () {
        this._audioPlayerService.loadAudios(this.currentPlaylist.id);
    };
    AudioPlayerComponent.prototype.selectAudio = function (audioId) {
        this.selectedAudioId = audioId;
        this.addToPlaylistVisible = true;
    };
    AudioPlayerComponent.prototype.closePlaylistDialog = function () {
        this.addToPlaylistVisible = false;
    };
    AudioPlayerComponent.prototype.addToPlaylist = function (playlistId) {
        return __awaiter(this, void 0, void 0, function () {
            var baseResult;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (!this.selectedAudioId) return [3 /*break*/, 2];
                        this.isAddToPlaylistError = false;
                        this.addToPlaylistError = null;
                        return [4 /*yield*/, this._audioPlayerService.addToPlaylist(playlistId, this.selectedAudioId)];
                    case 1:
                        baseResult = _a.sent();
                        this.isAddToPlaylistError = !baseResult.ok;
                        if (this.isAddToPlaylistError) {
                            this.addToPlaylistError = baseResult.info;
                        }
                        else {
                            this.addToPlaylistVisible = false;
                        }
                        _a.label = 2;
                    case 2: return [2 /*return*/];
                }
            });
        });
    };
    AudioPlayerComponent.prototype.removeFromPlaylist = function (audioId) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this._audioPlayerService.removeFromPlaylist(audioId)];
                    case 1:
                        _a.sent();
                        return [2 /*return*/];
                }
            });
        });
    };
    AudioPlayerComponent.prototype.onNgDestroy = function () {
        this._playlistLoadedSubscription.unsubscribe();
        this._audioChangedSubscription.unsubscribe();
        this._timeUpdateSubscription.unsubscribe();
        this._isLoopChangedSubscription.unsubscribe();
        this._actionChangedSubscription.unsubscribe();
        this._playlistChangedSubscription.unsubscribe();
        this._playlistUpdatedSubscription.unsubscribe();
    };
    return AudioPlayerComponent;
}());
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