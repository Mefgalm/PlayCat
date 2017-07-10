import { Injectable, Inject } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { SignUpRequest } from '../data/request/signUpRequest';
import { SignUpInResult } from '../data/response/signUpInResult';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class AuthService {
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
}