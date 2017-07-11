import { Injectable, Inject } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { SignInRequest } from '../data/request/signInRequest';
import { SignUpRequest } from '../data/request/signUpRequest';
import { SignUpInResult } from '../data/response/signUpInResult';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class AuthService {
    private _signInUrl = 'api/auth/signIn';
    private _signUpUrl = 'api/auth/signUp';
    private headers;

    constructor(private _http: Http) {
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
    }

    signUp(request: SignUpRequest): Promise<SignUpInResult> {

        var object = JSON.stringify(request);

        return this._http.post(this._signUpUrl, object, { headers: this.headers })
            .toPromise()
            .then(x => Object.assign(new SignUpInResult(), x.json()));
    }

    signIn(request: SignInRequest): Promise<SignUpInResult> {

        var object = JSON.stringify(request);

        return this._http.post(this._signInUrl, object, { headers: this.headers })
            .toPromise()
            .then(x => Object.assign(new SignUpInResult(), x.json()));
    }
}