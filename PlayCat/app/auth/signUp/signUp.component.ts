import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { FormService } from '../../shared/services/form.service';
import { ValidationService } from '../../shared/services/validation.service';
import { ValidationModel } from '../../data/validationModel';
import { SignUpRequest } from '../../data/request/signUpRequest';
import { SignUpInResult } from '../../data/response/signUpInResult';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
    selector: 'signUp',
    templateUrl: './app/auth/signUp/signUp.component.html',
    styleUrls: ['./app/auth/signUp/signUp.component.css'],
})

export class SignUpComponent {
    private modelName = 'SignUpRequest';

    public errors: Map<string, string>;
    public globalError: string;
    public errorsValidation: Map<string, Map<string, ValidationModel>>;
    public signUpForm: FormGroup;

    constructor(
        private _fb: FormBuilder,
        private _router: Router,
        private _authService: AuthService,
        private _formService: FormService,
        private _validationService: ValidationService
    ) {
        this.globalError = null;
        this.errors = new Map<string, string>();
        this.signUpForm = this._fb.group({
            firstName: [null],
            lastName: [null],
            email: [null],
            password: [null],
            confirmPassword: [null],
            verificationCode: [null],
        });
    }

    ngOnInit() {
        this._validationService
            .get(this.modelName)
            .then(res => {
                this.errorsValidation = res;
                this.signUpForm = this._formService.buildFormGroup(res);
            });
    }

    public save({ value, valid }: { value: any, valid: boolean }) {
        if (valid) {

            var request = new SignUpRequest(
                value.firstName,
                value.lastName,
                value.email,
                value.password,
                value.confirmPassword,
                value.verificationCode);

            this._authService
                .signUp(request)
                .then(signUpInResult => {
                    this.errors = signUpInResult.errors;
                    if (signUpInResult.ok) {
                        this._router.navigate(['/signIn']);
                    } else if (signUpInResult.showInfo) {
                        this.globalError = signUpInResult.info;
                    }
                });
        } else {
            this._formService.markControlsAsDirty(this.signUpForm);
        }
    }
}