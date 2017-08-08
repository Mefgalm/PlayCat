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
var UploadComponent = (function () {
    function UploadComponent(_fb, _uploadSerice, _validationService, _formService) {
        this._fb = _fb;
        this._uploadSerice = _uploadSerice;
        this._validationService = _validationService;
        this._formService = _formService;
        this._urlRequstModelName = 'UrlRequest';
        this._uploadAudioModelName = 'UploadAudioRequest';
        this.urlRequestErrorValiation = new Map();
        this.urlRequestForm = this._fb.group({
            url: [null],
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
    UploadComponent.prototype.videoInfo = function () {
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
        form_service_1.FormService])
], UploadComponent);
exports.UploadComponent = UploadComponent;
//# sourceMappingURL=upload.component.js.map