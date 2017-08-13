import { Component } from '@angular/core';
import { Playlist } from '../../../data/playlist';

@Component({
    selector: 'playlist',
    templateUrl: './app/workspace/music/playlist/playlist.component.html',
    styleUrls: ['./app/workspace/music/playlist/playlist.component.css'],
})
export class PlaylistComponent {
    public playlist: Playlist;

    constructor() {
        
    }

    ngOnInit() {
    }
}