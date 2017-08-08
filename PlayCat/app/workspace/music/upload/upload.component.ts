import { Component } from '@angular/core';
import { ValidationService } from '../../../shared/services/validation.service';
import { ValidationModel } from '../../../data/validationModel';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { UploadService } from './upload.service';
import { FormService } from '../../../shared/services/form.service';

@Component({
    selector: 'upload',
    templateUrl: './app/workspace/music/upload/upload.component.html',
    styleUrls: ['./app/workspace/music/upload/upload.component.css'],
})
export class UploadComponent {
    private _urlRequstModelName = 'UrlRequest';
    private _uploadAudioModelName = 'UploadAudioRequest';

    public urlRequestForm: FormGroup;
    public uploadAudioForm: FormGroup;

    public urlRequestErrorValiation: Map<string, Map<string, ValidationModel>>;
    public uploadAudioErrorValidation: Map<string, Map<string, ValidationModel>>;

    public url: string;

    constructor(
        private _fb: FormBuilder,
        private _uploadSerice: UploadService,
        private _validationService: ValidationService,
        private _formService: FormService) {

        this.urlRequestErrorValiation = new Map<string, Map<string, ValidationModel>>();

        this.urlRequestForm = this._fb.group({
            url: [null],
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

    videoInfo(): void {

    }


}