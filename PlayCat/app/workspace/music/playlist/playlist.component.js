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
var playlist_service_1 = require("./playlist.service");
var PlaylistComponent = (function () {
    function PlaylistComponent(_playlistService) {
        this._playlistService = _playlistService;
    }
    PlaylistComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._playlistService
            .getPlaylist(null, 0, 10)
            .then(function (playlistResult) {
            if (playlistResult.ok) {
                _this.playlist = playlistResult.playlist;
            }
            console.log(playlistResult);
        });
    };
    return PlaylistComponent;
}());
PlaylistComponent = __decorate([
    core_1.Component({
        selector: 'playlist',
        templateUrl: './app/workspace/music/playlist/playlist.component.html',
        styleUrls: ['./app/workspace/music/playlist/playlist.component.css'],
    }),
    __metadata("design:paramtypes", [playlist_service_1.PlaylistService])
], PlaylistComponent);
exports.PlaylistComponent = PlaylistComponent;
//# sourceMappingURL=playlist.component.js.map