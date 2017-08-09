import { Component } from '@angular/core';
import { ValidationService } from '../../../shared/services/validation.service';
import { ValidationModel } from '../../../data/validationModel';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { UploadService } from './upload.service';
import { GetInfoResult } from '../../../data/response/getInfoResult';
import { FormService } from '../../../shared/services/form.service';
import { UrlRequest } from '../../../data/request/urlRequest';
import { UploadAudioRequest } from '../../../data/request/uploadAudioRequest';

@Component({
    selector: 'upload',
    templateUrl: './app/workspace/music/upload/upload.component.html',
    styleUrls: ['./app/workspace/music/upload/upload.component.css'],
})
export class UploadComponent {
    private _urlRequstModelName = 'UrlRequest';
    private _uploadAudioModelName = 'UploadAudioRequest';

    private _url;
    private _isValidUrl;

    public isVideoInfoProcessing;
    public isAudioUploadProcessing;

    public urlRequestForm: FormGroup;
    public uploadAudioForm: FormGroup;

    public urlRequestErrorValiation: Map<string, Map<string, ValidationModel>>;
    public uploadAudioErrorValidation: Map<string, Map<string, ValidationModel>>;

    public contentLength: number;

    public videoInfoError: string;

    constructor(
        private _fb: FormBuilder,
        private _uploadSerice: UploadService,
        private _validationService: ValidationService,
        private _formService: FormService) {

        this.urlRequestErrorValiation = new Map<string, Map<string, ValidationModel>>();

        this.isVideoInfoProcessing = false;
        this.isAudioUploadProcessing = false;

        this._isValidUrl = false;
        this._url = null;

        this.urlRequestForm = this._fb.group({
            url: [null],
        });
        this.uploadAudioForm = this._fb.group({
            artist: [null],
            song: [null],
        });
    }

    ngOnInit() {
        this._validationService
            .get(this._urlRequstModelName)
            .then(res => {
                this.urlRequestErrorValiation = res;
                this.urlRequestForm = this._formService.buildFormGroup(res);
            });

        this._validationService
            .get(this._uploadAudioModelName)
            .then(res => {
                this.uploadAudioErrorValidation = res;
                this.uploadAudioForm = this._formService.buildFormGroup(res);
            });
    }

    private loadVideoInfo(getInfoResult: GetInfoResult) {
        this.uploadAudioForm.patchValue({
            artist: getInfoResult.urlInfo.artist,
            song: getInfoResult.urlInfo.song,
            url: this._url,
        });
    }

    videoInfo({ value, valid }: { value: any, valid: boolean }) {
        if (valid && !this.isVideoInfoProcessing) {
            this.isVideoInfoProcessing = true;

            let urlRequest = new UrlRequest(value.url);

            this._uploadSerice
                .videoInfo(urlRequest)
                .then(getInfoResult => {
                    this._isValidUrl = getInfoResult.ok;

                    if (getInfoResult.ok) {
                        this.contentLength = getInfoResult.urlInfo.contentLength;

                        this._url = value.url;
                        this.loadVideoInfo(getInfoResult);
                    } else {
                        this.contentLength = 0;

                        this._url = null;
                        if (getInfoResult.showInfo) {
                            this.videoInfoError = getInfoResult.info;
                        }
                    }

                    this.isVideoInfoProcessing = false;
                });
        } else {
            this._formService.markControlsAsDirty(this.urlRequestForm);
        }
    }

    uploadAudio({ value, valid }: { value: any, valid: boolean }) {
        if (valid && !this.isAudioUploadProcessing && this._isValidUrl) {
            this.isAudioUploadProcessing = true;

            let uploadAudioRequest = new UploadAudioRequest(
                value.artist,
                value.song,
                this._url
            );

            this._uploadSerice
                .uploadAudio(uploadAudioRequest)
                .then(baseResult => {
                    if (baseResult.ok) {
                        alert("Song added to your playlist");
                        console.log(baseResult);
                    } else {
                        this.urlRequestForm.clearValidators();
                        this.uploadAudioForm.clearValidators();

                        this._url = null;
                        this._isValidUrl = false;
                        this.isAudioUploadProcessing = false;
                    }
                });
        } else {
            this._formService.markControlsAsDirty(this.uploadAudioForm);
        }
    }
}