import { Injectable } from '@angular/core';
import { EventEmitter } from '@angular/core';
import { Playlist } from '../../data/playlist';
import { BaseResult } from '../../data/response/baseResult';
import { Audiotrack } from '../../data/audio';
import { AudioService } from '../music/audios/audios.service';
import { PlaylistService } from './playlist.service';
import { CreatePlaylistRequest } from '../../data/request/createPlaylistRequest';
import { UpdatePlaylistRequest } from '../../data/request/updatePlaylistRequest';
import { AudioResult } from "../../data/response/audioResult";

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

    private _audioStash: any;

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
            .then(async userPlaylistsResult => {
                if (userPlaylistsResult.ok) {
                    this._playlists = userPlaylistsResult.playlists;                   

                    for (let playlist of userPlaylistsResult.playlists) {
                        this._playlistAudiosCount.set(playlist.id, {
                            skip: playlist.audios.length,
                            isAllLoaded: false,
                        });
                    }

                    await this.selectPlaylist(null);

                    this._audioStash = {
                        audios: this._currentPlaylist.audios,
                        playlistId: this._currentPlaylist.id,
                    };


                    this.selectAudio(this._currentIndex);

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

            this.reloadAudioForPlaylistFromList(playlistId);
        }
    }

    async selectPlaylist(id: string) : Promise<void> {
        if (id) {
            this._currentPlaylist = this._playlists.find(x => x.id === id);
        } else {
            this._currentPlaylist = this._playlists.find(x => x.isGeneral);
        }
        let audioCount = this._playlistAudiosCount.get(this._currentPlaylist.id);

        if (audioCount.skip === 0 && !audioCount.isAllLoaded) {
            await this.loadAudios(this._currentPlaylist.id)
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
                    this._playlistAudiosCount.set(playlistResult.playlist.id, {
                        skip: playlistResult.playlist.audios.length,
                        isAllLoaded: false,
                    });
                    this.emitPlayerLoaded();
                }
            });
    }

    deletePlaylist(id: string) {
        this._playlistService
            .deletePlaylist(id)
            .then(async baseResult => {
                if (baseResult.ok) {
                    let index = this._playlists.findIndex(x => x.id == id);
                    this._playlists.splice(index, 1);

                    await this.selectPlaylist(null);

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
                    let audios = this._playlists[index].audios;

                    this._playlists[index] = playlistResult.playlist;
                    this._playlists[index].audios = audios;

                    this.emitPlayerLoaded();
                }
            });
    }

    async addToPlaylist(playlistId: string, audioId: string) {
        let index = this._playlists.findIndex(x => x.id == playlistId);

        let baseResult = await this._audioService.addAudioToPlaylist(playlistId, audioId);
        if (baseResult.ok) {
            //TODO check this
            this._playlistAudiosCount.set(playlistId, {
                skip: this._playlistAudiosCount.get(playlistId).skip + 1,
                isAllLoaded: false,
            });

            this.reloadAudioForPlaylistFromList(playlistId);
        }
        return baseResult;
    }

    async removeFromPlaylist(audioId: string): Promise<BaseResult> {
        let result = await this._audioService.removeFromPlaylist(this._currentPlaylist.id, audioId);

        this.reloadAudioForPlaylistFromList(this._currentPlaylist.id);

        return result;
    }

    loadAudios(playlistId: string): Promise<void> {
        let index = this._playlists.findIndex(x => x.id == playlistId);
        if (index !== -1 && !this._playlistAudiosCount.get(playlistId).isAllLoaded) {
            return this._audioService
                .loadAudios(playlistId, this._playlistAudiosCount.get(playlistId).skip, this._take)
                .then(audioResult => {
                    if (audioResult.ok) {
                        this._playlists[index].audios = this._playlists[index].audios.concat(audioResult.audios);                    
                        this._playlistAudiosCount.set(playlistId, {
                            skip: this._playlists[index].audios.length,
                            isAllLoaded: audioResult.audios.length === 0,
                        });
                        this.checkAndUpdateStash(playlistId, audioResult.audios);
                        this.onPlaylistUpdated.emit(this._playlists[index]);
                    }
                });
        }
    }

    public reloadAudioForPlaylistFromList(playlistId: string): Promise<void> {
        return this.reloadAudioForPlaylist(playlistId, 0, this._playlistAudiosCount.get(playlistId).skip);
    }

    public reloadAudioForPlaylist(playlistId: string, skip: number, take: number): Promise<void> {
        let index = this._playlists.findIndex(x => x.id == playlistId);
        if (index !== -1) {
            return this._audioService
                .loadAudios(playlistId, skip, take)
                .then(audioResult => {
                    if (audioResult.ok) {
                        this.checkAndUpdateStash(playlistId, audioResult.audios);

                        this._playlists[index].audios = audioResult.audios;
                        this.onPlaylistUpdated.emit(this._playlists[index]);
                    }
                });
        }
    }

    private checkAndUpdateStash(playlistId: string, audios: Audiotrack[]) {
        if (this._audioStash.playlistId === playlistId) {
            this._audioStash.audios = audios;
            this._currentIndex = audios.findIndex(x => x.id == this._currentAudio.id);
        }
    }

    private selectById(id: string) {
        this._audioStash = {
            audios: this._currentPlaylist.audios,
            playlistId: this._currentPlaylist.id,
        };

        let index = this._audioStash.audios.findIndex(a => a.id === id);
        if (index !== -1) {
            this.selectAudio(index);
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
        if (this._audioStash && this._audioStash.audios[index]) {
            this._currentIndex = index;
            this._currentAudio = this._audioStash.audios[index];

            this._audio.src = this._currentAudio.accessUrl;
        }
    }
}   