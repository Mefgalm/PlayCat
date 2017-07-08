import { Injectable, Inject } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { SignUpRequest } from '../data/request/signUpRequest';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class AuthService {
    private _signUpUrl = 'api/auth/signUp';
    private headers;

    constructor(private _http: Http) {
        this.headers = new Headers();
        this.headers.append('Content-Type', 'application/json');
    }

    signUp(): Promise<any> {
        var object = JSON.stringify(new SignUpRequest('vlad', 'kuz', 'mef@gmail.com', '234234gdfgf', '234234gdfgf', 'LMATJ-BQZBE-FBRZY-USVBJ'));

        return this._http.post(this._signUpUrl, object, { headers: this.headers })
        .toPromise()
        .then(this.extractData);
    }

    private extractData(res: Response): any {
        return res.json(); // body || {};
    }
}