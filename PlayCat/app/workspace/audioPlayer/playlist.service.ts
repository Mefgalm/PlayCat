import { Injectable } from '@angular/core';
import { HttpService } from '../../shared/services/http.service';
import { UserPlaylistsResult } from '../../data/response/userPlaylistsResult';
import { UrlParametr } from '../../data/urlParamert';
import { CreatePlaylistRequest } from '../../data/request/createPlaylistRequest';
import { UpdatePlaylistRequest } from '../../data/request/updatePlaylistRequest';
import { PlaylistResult } from '../../data/response/playlistResult';
import { BaseResult } from '../../data/response/baseResult';

@Injectable()
export class PlaylistService {
    private _playlistUrl = 'api/playlist/userPlaylists';
    private _createPlaylistUrl = 'api/playlist/create';
    private _updatePlaylistUrl = 'api/playlist/update';
    private _deletePlaylistUrl = 'api/playlist/delete';

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

    createPlaylist(createPlaylistRequest: CreatePlaylistRequest): Promise<PlaylistResult> {
        return this._httpService.post(this._createPlaylistUrl, JSON.stringify(createPlaylistRequest))
            .then(x => Object.assign(new PlaylistResult(), x.json()));
    }

    updatePlaylist(updatePlaylistRequest: UpdatePlaylistRequest): Promise<PlaylistResult> {
        return this._httpService.put(this._updatePlaylistUrl, JSON.stringify(updatePlaylistRequest))
            .then(x => Object.assign(new PlaylistResult(), x.json()));
    }

    deletePlaylist(playlistId: string): Promise<BaseResult> {
        return this._httpService.delete(this._deletePlaylistUrl + '/' + playlistId)
            .then(x => Object.assign(new BaseResult(), x.json()));
    }
}   