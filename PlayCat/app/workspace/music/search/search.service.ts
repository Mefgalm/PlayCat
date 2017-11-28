import { Injectable } from '@angular/core';
import { HttpService } from '../../../shared/services/http.service';
import { BaseResult } from '../../../data/response/baseResult';
import { UrlParametr } from '../../../data/urlParamert';
import { AudioResult } from '../../../data/response/audioResult';
import { AddRemovePlaylistRequest } from '../../../data/request/addRemovePlaylistRequest';

@Injectable()
export class SearchService {
    private _searchUrl = 'api/audio/search';

    constructor(
        private _httpService: HttpService) {
    }

    search(search: string, skip: number, take: number): Promise<AudioResult> {
        let parametrsLine = this._httpService.buildParametersUrl(
            new UrlParametr('search', search),
            new UrlParametr('skip', skip),
            new UrlParametr('take', take)
        );

        return this._httpService
            .get(this._searchUrl + parametrsLine)
            .then(x => Object.assign(new AudioResult(), x.json()));
    }
}   