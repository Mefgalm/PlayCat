import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/services/http.service';
import { UserPlaylistsResult } from '../../data/response/userPlaylistsResult';
import { UrlParametr } from '../../data/urlParamert';

@Injectable()
export class PlaylistService {
    private _playlistUrl = 'api/playlist/userPlaylists';

    constructor(private _httpService: HttpService) {
    }

    userPlaylists(playlistId: string, skip: number, take: number): Promise<UserPlaylistsResult> {
        let parametrsLine = this._httpService.buildParametersUrl(
            new UrlParametr('playlistId', playlistId),
            new UrlParametr('skip', skip),
            new UrlParametr('take', take)
        );

        return this._httpService
                   .get(this._playlistUrl + parametrsLine)
                   .then(x => Object.assign(new UserPlaylistsResult(), x.json()));
    } 
}   