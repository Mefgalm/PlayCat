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
var validation_service_1 = require("../../../shared/services/validation.service");
var forms_1 = require("@angular/forms");
var upload_service_1 = require("./upload.service");
var form_service_1 = require("../../../shared/services/form.service");
var urlRequest_1 = require("../../../data/request/urlRequest");
var uploadAudioRequest_1 = require("../../../data/request/uploadAudioRequest");
var audioPlayer_service_1 = require("../../audioPlayer/audioPlayer.service");
var UploadComponent = (function () {
    function UploadComponent(_fb, _uploadSerice, _validationService, _audioPlayerService, _formService) {
        this._fb = _fb;
        this._uploadSerice = _uploadSerice;
        this._validationService = _validationService;
        this._audioPlayerService = _audioPlayerService;
        this._formService = _formService;
        this._urlRequstModelName = 'UrlRequest';
        this._uploadAudioModelName = 'UploadAudioRequest';
        this.urlRequestErrorValiation = new Map();
        this.isVideoInfoProcessing = false;
        this.isAudioUploadProcessing = false;
        this.isUrlConfirm = false;
        this.url = null;
        this.urlRequestForm = this._fb.group({
            url: [null],
        });
        this.uploadAudioForm = this._fb.group({
            artist: [null],
            song: [null],
        });
    }
    UploadComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._validationService
            .get(this._urlRequstModelName)
            .then(function (res) {
            _this.urlRequestErrorValiation = res;
            _this.urlRequestForm = _this._formService.buildFormGroup(res);
        });
        this._validationService
            .get(this._uploadAudioModelName)
            .then(function (res) {
            _this.uploadAudioErrorValidation = res;
            _this.uploadAudioForm = _this._formService.buildFormGroup(res);
        });
    };
    UploadComponent.prototype.loadVideoInfo = function (getInfoResult) {
        this.uploadAudioForm.patchValue({
            artist: getInfoResult.urlInfo.artist,
            song: getInfoResult.urlInfo.song,
            url: this.url,
            videoId: getInfoResult.urlInfo.videoId,
        });
    };
    UploadComponent.prototype.videoInfo = function (_a) {
        var _this = this;
        var value = _a.value, valid = _a.valid;
        if (valid && !this.isVideoInfoProcessing) {
            this.isVideoInfoProcessing = true;
            var urlRequest = new urlRequest_1.UrlRequest(value.url);
            this.videoInfoError = null;
            this._uploadSerice
                .videoInfo(urlRequest)
                .then(function (getInfoResult) {
                _this.isUrlConfirm = getInfoResult.ok;
                if (getInfoResult.ok) {
                    _this.url = value.url;
                    _this.loadVideoInfo(getInfoResult);
                    _this.videoId = getInfoResult.urlInfo.videoId;
                }
                else {
                    _this.url = null;
                    if (getInfoResult.showInfo) {
                        _this.videoInfoError = getInfoResult.info;
                    }
                }
                _this.isVideoInfoProcessing = false;
            });
        }
        else {
            this._formService.markControlsAsDirty(this.urlRequestForm);
        }
    };
    UploadComponent.prototype.uploadAudio = function (_a) {
        var _this = this;
        var value = _a.value, valid = _a.valid;
        if (valid && !this.isAudioUploadProcessing && this.isUrlConfirm) {
            this.isAudioUploadProcessing = true;
            this.audioUploadError = null;
            var uploadAudioRequest = new uploadAudioRequest_1.UploadAudioRequest(value.artist, value.song, this.url);
            this._uploadSerice
                .uploadAudio(uploadAudioRequest)
                .then(function (uploadResult) {
                if (uploadResult.ok) {
                    _this.url = null;
                    _this.isUrlConfirm = false;
                    _this._audioPlayerService.uploadSong(null);
                    _this.urlRequestForm.patchValue({
                        url: null,
                    });
                    _this.urlRequestForm.reset();
                }
                else if (uploadResult.showInfo) {
                    _this.audioUploadError = uploadResult.info;
                }
                _this.isAudioUploadProcessing = false;
            });
        }
        else {
            this._formService.markControlsAsDirty(this.uploadAudioForm);
        }
    };
    return UploadComponent;
}());
UploadComponent = __decorate([
    core_1.Component({
        selector: 'upload',
        templateUrl: './app/workspace/music/upload/upload.component.html',
        styleUrls: ['./app/workspace/music/upload/upload.component.css'],
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder,
        upload_service_1.UploadService,
        validation_service_1.ValidationService,
        audioPlayer_service_1.AudioPlayerService,
        form_service_1.FormService])
], UploadComponent);
exports.UploadComponent = UploadComponent;
//# sourceMappingURL=upload.component.js.map