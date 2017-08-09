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
var playlistResult_1 = require("../../../data/response/playlistResult");
var urlParamert_1 = require("../../../data/urlParamert");
var PlaylistService = (function () {
    function PlaylistService(_httpService) {
        this._httpService = _httpService;
        this._playlistUrl = 'api/playlist/playlist';
    }
    PlaylistService.prototype.getPlaylist = function (playlistId, skip, take) {
        var parametrsLine = this._httpService.buildParametersUrl(new urlParamert_1.UrlParametr('playlistId', playlistId), new urlParamert_1.UrlParametr('skip', skip), new urlParamert_1.UrlParametr('take', take));
        return this._httpService.get(this._playlistUrl + parametrsLine)
            .then(function (x) { return Object.assign(new playlistResult_1.PlaylistResult(), x.json()); });
    };
    return PlaylistService;
}());
PlaylistService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_service_1.HttpService])
], PlaylistService);
exports.PlaylistService = PlaylistService;
//# sourceMappingURL=playlist.service.js.map