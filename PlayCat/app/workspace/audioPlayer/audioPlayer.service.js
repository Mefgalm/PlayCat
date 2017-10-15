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
var core_2 = require("@angular/core");
var audios_service_1 = require("../music/audios/audios.service");
var playlist_service_1 = require("./playlist.service");
var createPlaylistRequest_1 = require("../../data/request/createPlaylistRequest");
var updatePlaylistRequest_1 = require("../../data/request/updatePlaylistRequest");
var AudioPlayerService = (function () {
    function AudioPlayerService(_playlistService, _audioService) {
        var _this = this;
        this._playlistService = _playlistService;
        this._audioService = _audioService;
        this._take = 20;
        this._currentIndex = 0;
        this._audio = new Audio();
        this._isPlaying = false;
        this._playlistAudiosCount = new Map();
        this._audio.volume = 0.5;
        this.onPlayerLoaded = new core_2.EventEmitter();
        this.onTimeUpdate = new core_2.EventEmitter();
        this.onAudioChanged = new core_2.EventEmitter();
        this.onActionChanged = new core_2.EventEmitter();
        this.onIsLoopChanged = new core_2.EventEmitter();
        this.onPlaylistChanged = new core_2.EventEmitter();
        this.onPlaylistUpdated = new core_2.EventEmitter();
        this._audio.onended = function () {
            _this.next();
            _this.onAudioChanged.emit(_this._currentAudio);
        };
        this._audio.ontimeupdate = function () {
            _this._currentTime = _this._audio.currentTime;
            _this.onTimeUpdate.emit(_this._audio.currentTime);
        };
        this._audio.oncanplay = function () { return _this.onAudioChanged.emit(_this._currentAudio); };
        this._playlistService
            .userPlaylists(null, 0, this._take)
            .then(function (userPlaylistsResult) { return __awaiter(_this, void 0, void 0, function () {
            var _i, _a, playlist;
            return __generator(this, function (_b) {
                switch (_b.label) {
                    case 0:
                        if (!userPlaylistsResult.ok) return [3 /*break*/, 2];
                        this._playlists = userPlaylistsResult.playlists;
                        for (_i = 0, _a = userPlaylistsResult.playlists; _i < _a.length; _i++) {
                            playlist = _a[_i];
                            this._playlistAudiosCount.set(playlist.id, {
                                skip: playlist.audios.length,
                                isAllLoaded: false,
                            });
                        }
                        return [4 /*yield*/, this.selectPlaylist(null)];
                    case 1:
                        _b.sent();
                        this._audioStash = {
                            audios: this._currentPlaylist.audios,
                            playlistId: this._currentPlaylist.id,
                        };
                        this.selectAudio(this._currentIndex);
                        this.emitPlayerLoaded();
                        _b.label = 2;
                    case 2: return [2 /*return*/];
                }
            });
        }); });
    }
    //register events
    AudioPlayerService.prototype.getOnIsLoopEmitter = function () {
        return this.onIsLoopChanged;
    };
    AudioPlayerService.prototype.getOnPlaylistUpdated = function () {
        return this.onPlaylistUpdated;
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
    AudioPlayerService.prototype.getPlaylists = function () {
        return this._playlists ? this._playlists.slice() : this._playlists;
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
        if (currentTime >= 0 && currentTime <= this._currentAudio.duration) {
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
    AudioPlayerService.prototype.uploadSong = function (playlistId) {
        if (this._playlists) {
            if (!playlistId)
                playlistId = this._playlists.filter(function (x) { return x.isGeneral; })[0].id;
            this.reloadAudioForPlaylistFromList(playlistId);
        }
    };
    AudioPlayerService.prototype.selectPlaylist = function (id) {
        return __awaiter(this, void 0, void 0, function () {
            var audioCount;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (id) {
                            this._currentPlaylist = this._playlists.find(function (x) { return x.id === id; });
                        }
                        else {
                            this._currentPlaylist = this._playlists.find(function (x) { return x.isGeneral; });
                        }
                        audioCount = this._playlistAudiosCount.get(this._currentPlaylist.id);
                        if (!(audioCount.skip === 0 && !audioCount.isAllLoaded)) return [3 /*break*/, 2];
                        return [4 /*yield*/, this.loadAudios(this._currentPlaylist.id)];
                    case 1:
                        _a.sent();
                        _a.label = 2;
                    case 2:
                        this.onPlaylistChanged.emit(this._currentPlaylist);
                        return [2 /*return*/];
                }
            });
        });
    };
    AudioPlayerService.prototype.createPlaylist = function (title) {
        var _this = this;
        var createPlaylistRequest = new createPlaylistRequest_1.CreatePlaylistRequest(title);
        this._playlistService
            .createPlaylist(createPlaylistRequest)
            .then(function (playlistResult) {
            if (playlistResult.ok) {
                _this._playlists.push(playlistResult.playlist);
                _this._playlistAudiosCount.set(playlistResult.playlist.id, {
                    skip: playlistResult.playlist.audios.length,
                    isAllLoaded: false,
                });
                _this.emitPlayerLoaded();
            }
        });
    };
    AudioPlayerService.prototype.deletePlaylist = function (id) {
        var _this = this;
        this._playlistService
            .deletePlaylist(id)
            .then(function (baseResult) { return __awaiter(_this, void 0, void 0, function () {
            var index;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (!baseResult.ok) return [3 /*break*/, 2];
                        index = this._playlists.findIndex(function (x) { return x.id == id; });
                        this._playlists.splice(index, 1);
                        return [4 /*yield*/, this.selectPlaylist(null)];
                    case 1:
                        _a.sent();
                        this.emitPlayerLoaded();
                        _a.label = 2;
                    case 2: return [2 /*return*/];
                }
            });
        }); });
    };
    AudioPlayerService.prototype.updatePlaylist = function (id, title) {
        var _this = this;
        var updatePlaylistRequest = new updatePlaylistRequest_1.UpdatePlaylistRequest(id, title);
        this._playlistService
            .updatePlaylist(updatePlaylistRequest)
            .then(function (playlistResult) {
            if (playlistResult.ok) {
                var index = _this._playlists.findIndex(function (x) { return x.id == id; });
                var audios = _this._playlists[index].audios;
                _this._playlists[index] = playlistResult.playlist;
                _this._playlists[index].audios = audios;
                _this.emitPlayerLoaded();
            }
        });
    };
    AudioPlayerService.prototype.addToPlaylist = function (playlistId, audioId) {
        return __awaiter(this, void 0, void 0, function () {
            var index, baseResult;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        index = this._playlists.findIndex(function (x) { return x.id == playlistId; });
                        return [4 /*yield*/, this._audioService.addAudioToPlaylist(playlistId, audioId)];
                    case 1:
                        baseResult = _a.sent();
                        if (baseResult.ok) {
                            //TODO check this
                            this._playlistAudiosCount.set(playlistId, {
                                skip: this._playlistAudiosCount.get(playlistId).skip + 1,
                                isAllLoaded: false,
                            });
                            this.reloadAudioForPlaylistFromList(playlistId);
                        }
                        return [2 /*return*/, baseResult];
                }
            });
        });
    };
    AudioPlayerService.prototype.removeFromPlaylist = function (audioId) {
        return __awaiter(this, void 0, void 0, function () {
            var result;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, this._audioService.removeFromPlaylist(this._currentPlaylist.id, audioId)];
                    case 1:
                        result = _a.sent();
                        this.reloadAudioForPlaylistFromList(this._currentPlaylist.id);
                        return [2 /*return*/, result];
                }
            });
        });
    };
    AudioPlayerService.prototype.loadAudios = function (playlistId) {
        var _this = this;
        var index = this._playlists.findIndex(function (x) { return x.id == playlistId; });
        if (index !== -1 && !this._playlistAudiosCount.get(playlistId).isAllLoaded) {
            return this._audioService
                .loadAudios(playlistId, this._playlistAudiosCount.get(playlistId).skip, this._take)
                .then(function (audioResult) {
                if (audioResult.ok) {
                    _this._playlists[index].audios = _this._playlists[index].audios.concat(audioResult.audios);
                    _this._playlistAudiosCount.set(playlistId, {
                        skip: _this._playlists[index].audios.length,
                        isAllLoaded: audioResult.audios.length === 0,
                    });
                    _this.checkAndUpdateStash(playlistId, audioResult.audios);
                    _this.onPlaylistUpdated.emit(_this._playlists[index]);
                }
            });
        }
    };
    AudioPlayerService.prototype.reloadAudioForPlaylistFromList = function (playlistId) {
        return this.reloadAudioForPlaylist(playlistId, 0, this._playlistAudiosCount.get(playlistId).skip);
    };
    AudioPlayerService.prototype.reloadAudioForPlaylist = function (playlistId, skip, take) {
        var _this = this;
        var index = this._playlists.findIndex(function (x) { return x.id == playlistId; });
        if (index !== -1) {
            return this._audioService
                .loadAudios(playlistId, skip, take)
                .then(function (audioResult) {
                if (audioResult.ok) {
                    _this.checkAndUpdateStash(playlistId, audioResult.audios);
                    _this._playlists[index].audios = audioResult.audios;
                    _this.onPlaylistUpdated.emit(_this._playlists[index]);
                }
            });
        }
    };
    AudioPlayerService.prototype.checkAndUpdateStash = function (playlistId, audios) {
        var _this = this;
        if (this._audioStash.playlistId === playlistId) {
            this._audioStash.audios = audios;
            this._currentIndex = audios.findIndex(function (x) { return x.id == _this._currentAudio.id; });
        }
    };
    AudioPlayerService.prototype.selectById = function (id) {
        this._audioStash = {
            audios: this._currentPlaylist.audios,
            playlistId: this._currentPlaylist.id,
        };
        var index = this._audioStash.audios.findIndex(function (a) { return a.id === id; });
        if (index !== -1) {
            this.selectAudio(index);
        }
    };
    AudioPlayerService.prototype.emitPlayerLoaded = function () {
        this._playlists = this._playlists.sort(function (a, b) {
            if (a.isGeneral && !b.isGeneral)
                return -1;
            return (a.title > b.title) ? 1 : -1;
        });
        this.onPlayerLoaded.emit(this._playlists.slice());
    };
    AudioPlayerService.prototype.selectAudio = function (index) {
        if (this._audioStash && this._audioStash.audios[index]) {
            this._currentIndex = index;
            this._currentAudio = this._audioStash.audios[index];
            this._audio.src = this._currentAudio.accessUrl;
        }
    };
    return AudioPlayerService;
}());
AudioPlayerService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [playlist_service_1.PlaylistService,
        audios_service_1.AudioService])
], AudioPlayerService);
exports.AudioPlayerService = AudioPlayerService;
//# sourceMappingURL=audioPlayer.service.js.map