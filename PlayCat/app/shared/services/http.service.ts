import { Injectable } from '@angular/core';
import { Headers, Http, Response } from '@angular/http';
import { UrlParametr } from '../../data/urlParamert';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class HttpService {
    private headers;

    constructor(
        private _http: Http) {

        this.headers = new Headers();
        this.headers.set('Content-Type', 'application/json');    
    }

    updateAccessToken(accessToken: string) {
        this.headers.set('AccessToken', accessToken);        
    }

    post(url: string, jsonBody: string): Promise<Response> {
        return this._http.post(url, jsonBody, { headers: this.headers }).toPromise();
    }

    get(url: string): Promise<Response> {
        return this._http.get(url, { headers: this.headers }).toPromise();
    }

    delete(url: string): Promise<Response> {
        return this._http.delete(url, { headers: this.headers }).toPromise();
    }

    put(url: string, jsonBody: string): Promise<Response> {
        return this._http.put(url, jsonBody, { headers: this.headers }).toPromise();
    }

    buildParametersUrl(...args: UrlParametr[]): string {
        if (args === null || args.length === 0)
            return '';

        return '?' +
            args.filter(x => x.value !== null)
                .map(x => x.key + '=' + x.value)
                .join('&');
    }
}