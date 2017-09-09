import { Component } from '@angular/core';
import { ValidationService } from '../../../shared/services/validation.service';
import { ValidationModel } from '../../../data/validationModel';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { UploadService } from './upload.service';
import { GetInfoResult } from '../../../data/response/getInfoResult';
import { FormService } from '../../../shared/services/form.service';
import { UrlRequest } from '../../../data/request/urlRequest';
import { UploadAudioRequest } from '../../../data/request/uploadAudioRequest';
import { AudioPlayerService } from '../../audioPlayer/audioPlayer.service';

@Component({
    selector: 'upload',
    templateUrl: './app/workspace/music/upload/upload.component.html',
    styleUrls: ['./app/workspace/music/upload/upload.component.css'],
})
export class UploadComponent {
    private _urlRequstModelName = 'UrlRequest';
    private _uploadAudioModelName = 'UploadAudioRequest';

    public url;
    public isUrlConfirm;

    public isVideoInfoProcessing;
    public isAudioUploadProcessing;

    public urlRequestForm: FormGroup;
    public uploadAudioForm: FormGroup;

    public urlRequestErrorValiation: Map<string, Map<string, ValidationModel>>;
    public uploadAudioErrorValidation: Map<string, Map<string, ValidationModel>>;

    public videoInfoError: string;
    public audioUploadError: string;

    public videoId: string;

    constructor(
        private _fb: FormBuilder,
        private _uploadSerice: UploadService,
        private _validationService: ValidationService,
        private _audioPlayerService: AudioPlayerService,
        private _formService: FormService) {

        this.urlRequestErrorValiation = new Map<string, Map<string, ValidationModel>>();

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
            url: this.url,
            videoId: getInfoResult.urlInfo.videoId,
        });
    }

    videoInfo({ value, valid }: { value: any, valid: boolean }) {
        if (valid && !this.isVideoInfoProcessing) {
            this.isVideoInfoProcessing = true;

            let urlRequest = new UrlRequest(value.url);
            this.videoInfoError = null;

            this._uploadSerice
                .videoInfo(urlRequest)
                .then(getInfoResult => {
                    this.isUrlConfirm = getInfoResult.ok;

                    if (getInfoResult.ok) {

                        this.url = value.url;
                        this.loadVideoInfo(getInfoResult);
                        this.videoId = getInfoResult.urlInfo.videoId;
                    } else {

                        this.url = null;
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
        if (valid && !this.isAudioUploadProcessing && this.isUrlConfirm) {
            this.isAudioUploadProcessing = true;

            this.audioUploadError = null;

            let uploadAudioRequest = new UploadAudioRequest(
                value.artist,
                value.song,
                this.url
            );

            this._uploadSerice
                .uploadAudio(uploadAudioRequest)
                .then(uploadResult => {
                    if (uploadResult.ok) {
                        this.url = null;
                        this.isUrlConfirm = false;

                        this._audioPlayerService.uploadSong(uploadResult.audio, null);

                        this.urlRequestForm.patchValue({
                            url: null,
                        });
                        this.urlRequestForm.reset();
                    } else if (uploadResult.showInfo) {
                        this.audioUploadError = uploadResult.info;
                    }

                    this.isAudioUploadProcessing = false;
                });
        } else {
            this._formService.markControlsAsDirty(this.uploadAudioForm);
        }
    }
}