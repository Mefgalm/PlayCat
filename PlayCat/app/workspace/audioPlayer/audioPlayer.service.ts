import { Injectable } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Playlist } from '../../data/playlist';
import { Audiotrack } from '../../data/audio';
import { PlaylistService } from './playlist.service';

@Injectable()
export class AudioPlayerService {
    private readonly _take: number = 50;
    private _skip: number;

    private _playlist: Playlist;
    private _currentAudio: Audiotrack;
    private _currentIndex: number;

    private _audio: any;

    private readonly onPlaylistLoaded: EventEmitter<Playlist>;
    private readonly onDurationChange: EventEmitter<number>;
    private readonly onTimeUpdate: EventEmitter<number>;
    private readonly onAudioChanged: EventEmitter<Audiotrack>;
    private readonly onActionChanged: EventEmitter<boolean>;

    private _audioCount: number;
    private _isPlaying: boolean;

    private _currentTime: number;
    private _duration: number;

    constructor(private _playlistService: PlaylistService) {
        this._currentIndex = 0;
        this._skip = 0;

        this._audio = new Audio();
        this._isPlaying = false;

        this._audio.volume = 0.01;

        this.onPlaylistLoaded = new EventEmitter();
        this.onDurationChange = new EventEmitter();
        this.onTimeUpdate = new EventEmitter();
        this.onAudioChanged = new EventEmitter();
        this.onActionChanged = new EventEmitter();

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

        this._playlistService
            .getPlaylist(null, this._skip, this._take)
            .then(playlistResult => {
                if (playlistResult.ok) {
                    this._playlist = playlistResult.playlist;
                    this._audioCount = playlistResult.playlist.audios.length;
                    this.selectAudio(this._currentIndex);

                    this.onPlaylistLoaded.emit(this._playlist);
                }
            });
    }
    //register events
    //on event duration
    getOnDurationEmitter(): EventEmitter<number> {
        return this.onDurationChange;
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

    //playlist loaded
    getOnPlaylistLoadedEmitter(): EventEmitter<Playlist> {
        return this.onPlaylistLoaded;
    }

    //playlist info
    getPlaylist(): Playlist {
        return this._playlist;
    }

    getCurrentAudio(): Audiotrack {
        return this._currentAudio;
    }

    getCurrentTime(): number {
        return this._currentTime;
    }

    getDuration(): number {
        return this._duration;
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

    setVolume(volume: number) {
        this._audio.volume = volume;
    }

    setCurrentTime(currentTime: number) {
        this._audio.currentTime = currentTime;
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


    private selectById(id: string) {
        let audio = this._playlist.audios.find(a => a.id === id);
        if (audio) {
            this.selectAudio(this._playlist.audios.indexOf(audio));
        }
    }

    private selectAudio(index: number) {
        if (this._playlist && this._playlist.audios[index]) {
            this._currentIndex = index;
            this._currentAudio = this._playlist.audios[index];

            this._audio.src = this._currentAudio.accessUrl;

            this.onAudioChanged.emit(this._currentAudio);
        }
    }
}   