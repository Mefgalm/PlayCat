import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

import { SignUpRequest } from '../../data/request/signUpRequest';
import { SignUpInResult } from '../../data/response/signUpInResult';

@Component({
    selector: 'signUp',
    templateUrl: './app/auth/signUp/signUp.component.html',
    styleUrls: ['./app/auth/signUp/signUp.component.css'],
})

export class SignUpComponent {
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    confirmPassword: string;
    verificationCode: string;

    errors: Map<string, string[]>;
    globalError: string;

    constructor(private _authService: AuthService) {
        this.globalError = null;
        this.errors = new Map<string, string[]>();
    }

    public OnSubmit() {
        var request = new SignUpRequest(
            this.firstName,
            this.lastName,
            this.email,
            this.password,
            this.confirmPassword,
            this.verificationCode);

        this._authService
            .signUp(request)
            .then(signUpInResult => {
                this.errors = signUpInResult.errors;
                if (!signUpInResult.ok) {
                    this.globalError = signUpInResult.info;
                }
            });
    }
}