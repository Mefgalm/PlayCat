import { Component } from '@angular/core';
import { PlaylistService } from './playlist.service';
import { Playlist } from '../../../data/playlist';

@Component({
    selector: 'playlist',
    templateUrl: './app/workspace/music/playlist/playlist.component.html',
    styleUrls: ['./app/workspace/music/playlist/playlist.component.css'],
})
export class PlaylistComponent {
    public playlist: Playlist;

    constructor(private _playlistService: PlaylistService) {
        
    }

    ngOnInit() {
        this._playlistService
            .getPlaylist(null, 0, 10)
            .then(playlistResult => {
                if (playlistResult.ok) {
                    this.playlist = playlistResult.playlist;
                }
                console.log(playlistResult);
            });
    }
}