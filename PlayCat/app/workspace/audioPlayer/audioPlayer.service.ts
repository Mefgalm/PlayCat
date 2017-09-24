import { Injectable } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Playlist } from '../../data/playlist';
import { Audiotrack } from '../../data/audio';
import { AudioService } from '../music/audios/audios.service';
import { PlaylistService } from './playlist.service';
import { CreatePlaylistRequest } from '../../data/request/createPlaylistRequest';
import { UpdatePlaylistRequest } from '../../data/request/updatePlaylistRequest';

@Injectable()
export class AudioPlayerService {
    private readonly _take: number = 20;

    private _playlists: Playlist[];

    private _currentPlaylist: Playlist;

    private _currentAudio: Audiotrack;
    private _currentIndex: number;

    private _audio: any;

    private readonly onPlayerLoaded:    EventEmitter<Playlist[]>;
    private readonly onTimeUpdate:      EventEmitter<number>;
    private readonly onAudioChanged:    EventEmitter<Audiotrack>;
    private readonly onActionChanged:   EventEmitter<boolean>;
    private readonly onIsLoopChanged:   EventEmitter<boolean>;
    private readonly onPlaylistChanged: EventEmitter<Playlist>;
    private readonly onPlaylistUpdated: EventEmitter<Playlist>;

    private _isPlaying: boolean;

    private _currentTime: number;
    private _duration: number;

    private _playlistAudiosCount: Map<string, any>;

    constructor(
        private _playlistService: PlaylistService,
        private _audioService: AudioService
    ) {
        this._currentIndex = 0;

        this._audio = new Audio();
        this._isPlaying = false;

        this._playlistAudiosCount = new Map<string, any>();

        this._audio.volume = 0.5;

        this.onPlayerLoaded =    new EventEmitter<Playlist[]>();
        this.onTimeUpdate =      new EventEmitter<number>();
        this.onAudioChanged =    new EventEmitter<Audiotrack>();
        this.onActionChanged =   new EventEmitter<boolean>();
        this.onIsLoopChanged =   new EventEmitter<boolean>();
        this.onPlaylistChanged = new EventEmitter<Playlist>();
        this.onPlaylistUpdated = new EventEmitter<Playlist>();

        this._audio.onended = () => {
            this.next();
            this.onAudioChanged.emit(this._currentAudio);
        };

        this._audio.ontimeupdate = () => {
            this._currentTime = this._audio.currentTime;
            this.onTimeUpdate.emit(this._audio.currentTime);
        };

        this._audio.oncanplay = () => this.onAudioChanged.emit(this._currentAudio);

        this._playlistService
            .userPlaylists(null, 0, this._take)
            .then(userPlaylistsResult => {
                if (userPlaylistsResult.ok) {
                    this._playlists = userPlaylistsResult.playlists;

                    this.selectPlaylist(null);
                    this.selectAudio(this._currentIndex);

                    this._playlistAudiosCount.set(this._currentPlaylist.id, {
                        skip: this._currentPlaylist.audios.length,
                        isAllLoaded: false,
                    });
                    this.emitPlayerLoaded();
                }
            });
    }
    //register events
    getOnIsLoopEmitter(): EventEmitter<boolean> {
        return this.onIsLoopChanged;
    }

    getOnPlaylistUpdated(): EventEmitter<Playlist> {
        return this.onPlaylistUpdated;
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

    getPlaylists(): Playlist[] {
        return this._playlists ? this._playlists.slice() : this._playlists;
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
        if (currentTime >= 0 && currentTime <= this._currentAudio.duration) {
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

    uploadSong(playlistId: string) {
        if (this._playlists) {
            if (!playlistId)
                playlistId = this._playlists.filter(x => x.isGeneral)[0].id;

            this.reloadAudioForPlaylist(playlistId, 0, this._playlistAudiosCount.get(playlistId).skip);
        }
    }

    selectPlaylist(id: string) {
        if (id) {
            this._currentPlaylist = this._playlists.find(x => x.id === id);
        } else {
            this._currentPlaylist = this._playlists.find(x => x.isGeneral);
        }
        this.onPlaylistChanged.emit(this._currentPlaylist);
    }

    createPlaylist(title: string) {
        let createPlaylistRequest = new CreatePlaylistRequest(title);

        this._playlistService
            .createPlaylist(createPlaylistRequest)
            .then(playlistResult => {
                if (playlistResult.ok) {
                    this._playlists.push(playlistResult.playlist);          
                    this.emitPlayerLoaded();
                }
            });
    }

    deletePlaylist(id: string) {
        this._playlistService
            .deletePlaylist(id)
            .then(baseResult => {
                if (baseResult.ok) {
                    let index = this._playlists.findIndex(x => x.id == id);
                    this._playlists.splice(index, 1);

                    this.emitPlayerLoaded();
                }                
            });
    }

    updatePlaylist(id: string, title: string) {
        let updatePlaylistRequest = new UpdatePlaylistRequest(id, title);

        this._playlistService
            .updatePlaylist(updatePlaylistRequest)
            .then(playlistResult => {
                if (playlistResult.ok) {
                    let index = this._playlists.findIndex(x => x.id == id);
                    this._playlists[index] = playlistResult.playlist;

                    this.emitPlayerLoaded();
                }
            });
    }

    loadAudios(playlistId: string) {
        let index = this._playlists.findIndex(x => x.id == playlistId);
        if (index !== -1 && !this._playlistAudiosCount.get(playlistId).isAllLoaded) {
            this._audioService
                .loadAudios(playlistId, this._playlistAudiosCount.get(playlistId).skip, this._take)
                .then(audioResult => {
                    if (audioResult.ok) {
                        this._playlists[index].audios = this._playlists[index].audios.concat(audioResult.audios);                    
                        this._playlistAudiosCount.set(playlistId, {
                            skip: this._playlists[index].audios.length,
                            isAllLoaded: audioResult.audios.length === 0,
                        });
                        this.onPlaylistUpdated.emit(this._playlists[index]);
                    }
                });
        }
    }

    private reloadAudioForPlaylist(playlistId: string, skip: number, take: number) {
        let index = this._playlists.findIndex(x => x.id == playlistId);
        if (index !== -1) {
            this._audioService
                .loadAudios(playlistId, skip, take)
                .then(audioResult => {
                    if (audioResult.ok) {
                        this._playlists[index].audios = audioResult.audios;
                        this.onPlaylistUpdated.emit(this._playlists[index]);
                    }
                });
        }
    }

    private selectById(id: string) {
        let audio = this._currentPlaylist.audios.find(a => a.id === id);
        if (audio) {
            this.selectAudio(this._currentPlaylist.audios.indexOf(audio));
        }
    }

    private emitPlayerLoaded() {
        this._playlists = this._playlists.sort((a, b) => {
            if (a.isGeneral && !b.isGeneral)
                return -1;

            return (a.title > b.title) ? 1 : -1;
        });
        this.onPlayerLoaded.emit(this._playlists.slice());
    }

    private selectAudio(index: number) {
        if (this._currentPlaylist && this._currentPlaylist.audios[index]) {
            this._currentIndex = index;
            this._currentAudio = this._currentPlaylist.audios[index];

            this._audio.src = this._currentAudio.accessUrl;
        }
    }
}   