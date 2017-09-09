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
var getInfoResult_1 = require("../../../data/response/getInfoResult");
var uploadResult_1 = require("../../../data/response/uploadResult");
var UploadService = (function () {
    function UploadService(_httpService) {
        this._httpService = _httpService;
        this._videoInfoUrl = 'api/upload/videoInfo';
        this._uploadAudioUrl = 'api/upload/uploadAudio';
    }
    UploadService.prototype.videoInfo = function (urlRequest) {
        return this._httpService.post(this._videoInfoUrl, JSON.stringify(urlRequest))
            .then(function (x) { return Object.assign(new getInfoResult_1.GetInfoResult(), x.json()); });
    };
    UploadService.prototype.uploadAudio = function (uploadAudioRequest) {
        return this._httpService.post(this._uploadAudioUrl, JSON.stringify(uploadAudioRequest))
            .then(function (x) { return Object.assign(new uploadResult_1.UploadResult(), x.json()); });
    };
    return UploadService;
}());
UploadService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_service_1.HttpService])
], UploadService);
exports.UploadService = UploadService;
//# sourceMappingURL=upload.service.js.map