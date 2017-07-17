import { Injectable } from '@angular/core';
import { HttpService } from '../services/http.service';

@Injectable()
export class ValidationService {
    private _validationRulesUrl = 'api/validation/validateRules/';

    constructor(private _httpService: HttpService) {
    }

    get(modelType: string): Promise<Map<string, any>> {
        return this._httpService.get(this._validationRulesUrl + modelType)
            .then(x => x.json() as Map<string, any>);
    }
}