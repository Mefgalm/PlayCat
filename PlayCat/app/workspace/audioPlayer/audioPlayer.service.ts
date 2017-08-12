import { Injectable } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Playlist } from '../../data/playlist';
import { Audio } from '../../data/audio';

@Injectable()
export class AudioPlayerService {

    private _playlist: Playlist;
    private _currentAudio: Audio;
    private _currentIndex: number;

    public onLoaded: EventEmitter<boolean> = new EventEmitter();

    private _audioCount;

    constructor() {
        this._currentIndex = 0;
    }

    emitOnLoadedEvent(number) {
        this.onLoaded.emit(number);
    }

    getOnLoadedEmitter() {
        return this.onLoaded;
    }

    setPlaylist(playlist: Playlist) {
        this._playlist = playlist;
        this._audioCount = playlist.audios.length;
        this.playAudio(this._currentIndex);

        this.emitOnLoadedEvent(true);
    }

    isLoaded(): boolean {
        return this._playlist !== null;
    }

    getAudios(): Audio[] {
        if (this._playlist) {
            return this._playlist.audios;
        }
    }

    getTitle() {
        if (this._playlist) {
            return this._playlist.title;
        }
    }

    playAudio(index: number) {        
        if (this._playlist && this._playlist.audios[index]) {
            this._currentIndex = index;
            this._currentAudio = this._playlist.audios[index];
        }
    }

    getArtist(): string {
        return this._currentAudio.artist;
    }

    getSong(): string {
        return this._currentAudio.song;
    }

    getCurrentAudio(): Audio {
        return this._currentAudio;
    }

    playNext() {
        this.playAudio(this._currentIndex + 1);
    }

    playPrev() {
        this.playAudio(this._currentIndex - 1);
    }
}   