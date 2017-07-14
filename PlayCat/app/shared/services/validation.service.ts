import { Injectable } from '@angular/core';
import { HttpService } from '../services/http.service';

@Injectable()
export class ValidationService {
    private _signInUrl = 'api/validation/validateRules/';

    constructor(private _httpService: HttpService) {
    }

    get(modelType: string): any {
        return this._httpService.get(this._signInUrl + modelType)
            .then(x => x.json());
    }
}