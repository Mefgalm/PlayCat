﻿import { Injectable } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Playlist } from '../../data/playlist';
import { Audiotrack } from '../../data/audio';
import { PlaylistService } from './playlist.service';

@Injectable()
export class AudioPlayerService {
    private readonly _take: number = 50;
    private _skip: number;

    private _playlists: Playlist[];

    private _currentPlaylist: Playlist;

    private _currentAudio: Audiotrack;
    private _currentIndex: number;

    private _audio: any;

    private readonly onPlayerLoaded: EventEmitter<Playlist[]>;
    private readonly onDurationChange: EventEmitter<number>;
    private readonly onTimeUpdate: EventEmitter<number>;
    private readonly onAudioChanged: EventEmitter<Audiotrack>;
    private readonly onActionChanged: EventEmitter<boolean>;
    private readonly onIsLoopChanged: EventEmitter<boolean>;
    private readonly onPlaylistChanged: EventEmitter<Playlist>;

    private _isPlaying: boolean;

    private _currentTime: number;
    private _duration: number;

    constructor(private _playlistService: PlaylistService) {
        this._currentIndex = 0;
        this._skip = 0;

        this._audio = new Audio();
        this._isPlaying = false;

        this._audio.volume = 0.5;

        this.onPlayerLoaded = new EventEmitter();
        this.onDurationChange = new EventEmitter();
        this.onTimeUpdate = new EventEmitter();
        this.onAudioChanged = new EventEmitter();
        this.onActionChanged = new EventEmitter();
        this.onIsLoopChanged = new EventEmitter();
        this.onPlaylistChanged = new EventEmitter();

        this._audio.onended = () => {
            this.next();
            this.onAudioChanged.emit(this._currentAudio);
        };

        this._audio.ontimeupdate = () => {
            this._currentTime = this._audio.currentTime;
            this.onTimeUpdate.emit(this._audio.currentTime);
        };
        this._audio.ondurationchange = () => {
            this._duration = this._audio.duration;
            this.onDurationChange.emit(this._audio.duration);
        }

        this._audio.oncanplay = () => this.onAudioChanged.emit(this._currentAudio);

        this._playlistService
            .userPlaylists(null, this._skip, this._take)
            .then(userPlaylistsResult => {
                if (userPlaylistsResult.ok) {
                    this._playlists = userPlaylistsResult.playlists;
                    
                    this.selectPlaylist(null);
                    this.selectAudio(this._currentIndex);

                    this.onPlayerLoaded.emit(this._playlists);
                }
            });
    }
    //register events
    //on event duration
    getOnDurationEmitter(): EventEmitter<number> {
        return this.onDurationChange;
    }

    getOnIsLoopEmitter(): EventEmitter<boolean> {
        return this.onIsLoopChanged;
    }

    //time update
    getOnTimeUpdateEmitter(): EventEmitter<number> {
        return this.onTimeUpdate;
    }

    getOnActionChanged(): EventEmitter<boolean> {
        return this.onActionChanged;
    }

    //audio
    getOnAudioChanged(): EventEmitter<Audiotrack> {
        return this.onAudioChanged;
    }

    getOnPlaylistChanged(): EventEmitter<Playlist> {
        return this.onPlaylistChanged;
    }

    //playlist loaded
    getOnPlayerLoadedEmitter(): EventEmitter<Playlist[]> {
        return this.onPlayerLoaded;
    }

    //playlist info
    getPlaylist(): Playlist {
        return this._currentPlaylist;
    }

    getCurrentAudio(): Audiotrack {
        return this._currentAudio;
    }

    isLoop(): boolean {
        return this._audio.loop;
    }

    getCurrentTime(): number {
        return this._currentTime;
    }

    getDuration(): number {
        return this._duration;
    }

    getPlaylists(): Playlist[] {
        return this._playlists;
    }

    getCurrentPlaylist(): Playlist {
        return this._currentPlaylist;
    }

    //controls
    play() {        
        this._audio.play();

        this._isPlaying = true;
        this.onActionChanged.emit(this._isPlaying);
    }

    playById(id: string) {
        this.selectById(id);
        this.play();
    }

    pause() {
        this._audio.pause();

        this._isPlaying = false;
        this.onActionChanged.emit(this._isPlaying);
    }

    isPlaying(): boolean {
        return this._isPlaying;
    }

    setIsLoop(isLoop: boolean) {
        this._audio.loop = isLoop;
        this.onIsLoopChanged.emit(this._audio.loop);
    }

    setVolume(volume: number) {
        if (volume >= 0 && volume <= 100) {
            this._audio.volume = volume;
        }
    }

    getVolume(): number {
        return this._audio.volume;
    }

    setCurrentTime(currentTime: number) {
        if (currentTime >= 0 && currentTime <= this._duration) {
            this._audio.currentTime = currentTime;
        }
    }

    next() {
        this.selectAudio(this._currentIndex + 1);

        if (this._isPlaying)
            this.play();
    }

    previous() {
        this.selectAudio(this._currentIndex - 1);

        if (this._isPlaying)
            this.play();
    }

    selectPlaylist(id: string) {
        if (id) {
            this._currentPlaylist = this._playlists.find(x => x.id === id);
        } else {
            this._currentPlaylist = this._playlists.find(x => x.isGeneral);
        }
        this.onPlaylistChanged.emit(this._currentPlaylist);
    }

    private selectById(id: string) {
        let audio = this._currentPlaylist.audios.find(a => a.id === id);
        if (audio) {
            this.selectAudio(this._currentPlaylist.audios.indexOf(audio));
        }
    }

    private selectAudio(index: number) {
        if (this._currentPlaylist && this._currentPlaylist.audios[index]) {
            this._currentIndex = index;
            this._currentAudio = this._currentPlaylist.audios[index];

            this._audio.src = this._currentAudio.accessUrl;
        }
    }
}   