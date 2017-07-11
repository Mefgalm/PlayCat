import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

import { SignInRequest } from '../../data/request/signInRequest';
import { SignUpInResult } from '../../data/response/signUpInResult';

@Component({
    selector: 'signIn',
    templateUrl: './app/auth/signIn/signIn.component.html',
    styleUrls: ['./app/auth/signIn/signIn.component.css'],
})

export class SignInComponent {
    email: string;
    password: string;

    errors: Map<string, string[]>;
    globalError: string;

    constructor(private _authService: AuthService) {
        this.globalError = null;
        this.errors = new Map<string, string[]>();
    }

    public OnSubmit() {
        var request = new SignInRequest(
            this.email,
            this.password);

        this._authService
            .signIn(request)
            .then(signUpInResult => {
                console.log(signUpInResult);
                this.errors = signUpInResult.errors;
                if (!signUpInResult.ok) {
                    this.globalError = signUpInResult.info;
                }
            });
    }
}