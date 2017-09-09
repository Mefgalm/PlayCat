import { Injectable } from '@angular/core';
import { HttpService } from '../../../shared/services/http.service';
import { UrlRequest } from '../../../data/request/urlRequest';
import { UploadAudioRequest } from '../../../data/request/uploadAudioRequest';
import { GetInfoResult } from '../../../data/response/getInfoResult';
import { UploadResult } from '../../../data/response/uploadResult';
import { BaseResult } from '../../../data/response/baseResult';

@Injectable()
export class UploadService {
    private _videoInfoUrl = 'api/upload/videoInfo';
    private _uploadAudioUrl = 'api/upload/uploadAudio';

    constructor(private _httpService: HttpService) {
    }

    videoInfo(urlRequest: UrlRequest): Promise<GetInfoResult> {
        return this._httpService.post(this._videoInfoUrl, JSON.stringify(urlRequest))
            .then(x => Object.assign(new GetInfoResult(), x.json()));
    }
    uploadAudio(uploadAudioRequest: UploadAudioRequest) : Promise<UploadResult> {
        return this._httpService.post(this._uploadAudioUrl, JSON.stringify(uploadAudioRequest))
            .then(x => Object.assign(new UploadResult(), x.json()));
    }
}   