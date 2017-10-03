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
var http_service_1 = require("../../../shared/services/http.service");
var baseResult_1 = require("../../../data/response/baseResult");
var urlParamert_1 = require("../../../data/urlParamert");
var audioResult_1 = require("../../../data/response/audioResult");
var addRemovePlaylistRequest_1 = require("../../../data/request/addRemovePlaylistRequest");
var AudioService = (function () {
    function AudioService(_httpService) {
        this._httpService = _httpService;
        this._audioUrl = 'api/audio/audios';
        this._addToPlaylistyUrl = 'api/audio/addToPlaylist';
        this._removeFromPlaylistyUrl = 'api/audio/removeFromPlaylist';
    }
    AudioService.prototype.loadAudios = function (playlistId, skip, take) {
        var parametrsLine = this._httpService.buildParametersUrl(new urlParamert_1.UrlParametr('playlistId', playlistId), new urlParamert_1.UrlParametr('skip', skip), new urlParamert_1.UrlParametr('take', take));
        return this._httpService
            .get(this._audioUrl + parametrsLine)
            .then(function (x) { return Object.assign(new audioResult_1.AudioResult(), x.json()); });
    };
    AudioService.prototype.addAudioToPlaylist = function (playlistId, audioId) {
        var addRemovePlaylist = new addRemovePlaylistRequest_1.AddRemovePlaylistRequest(playlistId, audioId);
        return this._httpService
            .put(this._addToPlaylistyUrl, JSON.stringify(addRemovePlaylist))
            .then(function (x) { return Object.assign(new baseResult_1.BaseResult(), x.json()); });
    };
    AudioService.prototype.removeFromPlaylist = function (playlistId, audioId) {
        var addRemovePlaylist = new addRemovePlaylistRequest_1.AddRemovePlaylistRequest(playlistId, audioId);
        return this._httpService
            .put(this._removeFromPlaylistyUrl, JSON.stringify(addRemovePlaylist))
            .then(function (x) { return Object.assign(new baseResult_1.BaseResult(), x.json()); });
    };
    return AudioService;
}());
AudioService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_service_1.HttpService])
], AudioService);
exports.AudioService = AudioService;
//# sourceMappingURL=audios.service.js.map