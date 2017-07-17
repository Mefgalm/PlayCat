import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormService } from '../../shared/services/form.service';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { ValidationService } from '../../shared/services/validation.service';

import { ErrorComponent } from '../../shared/components/error.component';

import { SignInRequest } from '../../data/request/signInRequest';
import { SignUpInResult } from '../../data/response/signUpInResult';

@Component({
    selector: 'signIn',
    templateUrl: './app/auth/signIn/signIn.component.html',
    styleUrls: ['./app/auth/signIn/signIn.component.css'],
})

export class SignInComponent {
    private modelName = 'signUpRequest';

    public email: string;
    public password: string;

    public errors: Map<string, string>;
    public globalError: string;

    public errorsValidation: Map<string, any>;

    public signInForm: FormGroup;

    constructor(
        private _fb: FormBuilder, 
        private _authService: AuthService,
        private _formService: FormService,
        private _validationService: ValidationService
    ) {
        this.globalError = null;
        this.errors = new Map<string, string>();
        this.errorsValidation = new Map<string, any>();

        this.errorsValidation['email'] = {
            required: 'Field email is required',
            pattern: 'Pattern is invalid'
        };

        this.errorsValidation['password'] = {
            required: 'Field password is required',
        };
    }

    ngOnInit() {
        this._validationService
            .get(this.modelName)
            .then(res => this.errorsValidation = res);

        this.signInForm = this._fb.group({
            email: [null, Validators.required],
            password: [null, Validators.required]
        });
    }

    public save({ value, valid }: { value: any, valid: boolean }) {
        if (valid) {
            var request = new SignInRequest(
                value.email,
                value.password);

            this._authService
                .signIn(request)
                .then(signUpInResult => {
                    console.log(signUpInResult);
                    this.errors = signUpInResult.errors;
                    if (!signUpInResult.ok) {
                        this.globalError = signUpInResult.info;
                    }
                });
        } else {
            this._formService.markControlsAsDirty(this.signInForm);
        }
    }
}