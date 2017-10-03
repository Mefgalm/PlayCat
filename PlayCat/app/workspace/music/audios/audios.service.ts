import { Injectable } from '@angular/core';
import { HttpService } from '../../../shared/services/http.service';
import { BaseResult } from '../../../data/response/baseResult';
import { UrlParametr } from '../../../data/urlParamert';
import { AudioResult } from '../../../data/response/audioResult';
import { AddRemovePlaylistRequest } from '../../../data/request/addRemovePlaylistRequest';

@Injectable()
export class AudioService {
    private _audioUrl = 'api/audio/audios';
    private _addToPlaylistyUrl = 'api/audio/addToPlaylist';
    private _removeFromPlaylistyUrl = 'api/audio/removeFromPlaylist';

    constructor(private _httpService: HttpService) {
    }

    loadAudios(playlistId: string, skip: number, take: number): Promise<AudioResult> {
        let parametrsLine = this._httpService.buildParametersUrl(
            new UrlParametr('playlistId', playlistId),
            new UrlParametr('skip', skip),
            new UrlParametr('take', take)
        );

        return this._httpService
            .get(this._audioUrl + parametrsLine)
            .then(x => Object.assign(new AudioResult(), x.json()));
    }

    addAudioToPlaylist(playlistId: string, audioId: string): Promise<BaseResult> {
        let addRemovePlaylist = new AddRemovePlaylistRequest(playlistId, audioId);

        return this._httpService
            .put(this._addToPlaylistyUrl, JSON.stringify(addRemovePlaylist))
            .then(x => Object.assign(new BaseResult(), x.json()));
    }


    removeFromPlaylist(playlistId: string, audioId: string): Promise<BaseResult> {
        let addRemovePlaylist = new AddRemovePlaylistRequest(playlistId, audioId);

        return this._httpService
            .put(this._removeFromPlaylistyUrl, JSON.stringify(addRemovePlaylist))
            .then(x => Object.assign(new BaseResult(), x.json()));
    }
}   