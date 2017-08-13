import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/services/http.service';
import { PlaylistResult } from '../../data/response/playlistResult';
import { UrlParametr } from '../../data/urlParamert';

@Injectable()
export class PlaylistService {
    private _playlistUrl = 'api/playlist/playlist';

    constructor(private _httpService: HttpService) {
    }

    getPlaylist(playlistId: string, skip: number, take: number): Promise<PlaylistResult> {
        let parametrsLine = this._httpService.buildParametersUrl(
            new UrlParametr('playlistId', playlistId),
            new UrlParametr('skip', skip),
            new UrlParametr('take', take)
        );

        return this._httpService.get(this._playlistUrl + parametrsLine)
            .then(x => Object.assign(new PlaylistResult(), x.json()));
    } 
}   