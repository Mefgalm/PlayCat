import { Injectable, Inject } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { SignInRequest } from '../data/request/signInRequest';
import { SignUpRequest } from '../data/request/signUpRequest';
import { SignUpInResult } from '../data/response/signUpInResult';
import { HttpService } from '../shared/services/http.service';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class AuthService {
    private _signInUrl = 'api/auth/signIn';
    private _signUpUrl = 'api/auth/signUp';

    constructor(private _httpService: HttpService) {
    }

    signUp(request: SignUpRequest): Promise<SignUpInResult> {
        return this._httpService.post(this._signUpUrl, JSON.stringify(request))
            .then(x => Object.assign(new SignUpInResult(), x.json()));
    }

    signIn(request: SignInRequest): Promise<SignUpInResult> {
        return this._httpService.post(this._signInUrl, JSON.stringify(request))
            .then(x => Object.assign(new SignUpInResult(), x.json()));
    }
}