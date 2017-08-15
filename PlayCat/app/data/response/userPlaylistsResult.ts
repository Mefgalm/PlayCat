import { BaseResult } from './baseResult';
import { Playlist } from '../playlist';

export class UserPlaylistsResult extends BaseResult {
    public playlists: Playlist[];
}