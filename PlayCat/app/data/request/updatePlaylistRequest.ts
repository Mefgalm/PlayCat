import { CreatePlaylistRequest } from './createPlaylistRequest';

export class UpdatePlaylistRequest extends CreatePlaylistRequest {    
    constructor(public playlistId: string, title: string) {
        super(title);
    }
}