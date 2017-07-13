import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { FormService } from '../../shared/services/form.service';

import { SignUpRequest } from '../../data/request/signUpRequest';
import { SignUpInResult } from '../../data/response/signUpInResult';

@Component({
    selector: 'signUp',
    templateUrl: './app/auth/signUp/signUp.component.html',
    styleUrls: ['./app/auth/signUp/signUp.component.css'],
})

export class SignUpComponent {
    public errors: Map<string, string>;
    public globalError: string;

    public signUpForm: FormGroup;

    constructor(
        private _fb: FormBuilder,
        private _authService: AuthService,
        private _formService: FormService) {

        this.globalError = null;
        this.errors = new Map<string, string>();
    }

    ngOnInit() {
        this.signUpForm = this._fb.group({
            firstName: [null, Validators.compose([Validators.required, Validators.pattern('^vl')])],
            lastName: [null, Validators.required],
            email: [null, Validators.required],
            password: [null, Validators.required],
            confirmPassword: [null, Validators.required],
            verificationCode: [null, Validators.required],
        });
    }

    public save({ value, valid }: { value: any, valid: boolean }) {


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
                console.log(signUpInResult);
                this.errors = signUpInResult.errors;
                if (!signUpInResult.ok) {
                    this.globalError = signUpInResult.info;
                }
            });
    }
}