import { Component, ViewChild } from '@angular/core';
import { AudioPlayerService } from './audioPlayer.service';
import { Playlist } from '../../data/playlist';
import { Audiotrack } from '../../data/audio';

@Component({
    selector: 'audioPlayer',
    templateUrl: './app/workspace/audioPlayer/audioPlayer.component.html',
    styleUrls: [
        './app/workspace/audioPlayer/audioPlayer.component.css', 
    ],
})
export class AudioPlayerComponent {
    @ViewChild('progressBar') progressBar;
    private _timeUpdateSubscription: any;
    private _audioChangedSubscription: any;
    private _actionChangedSubscription: any;
    private _durationChangeSubscription: any;
    private _playlistLoadedSubscription: any;
    private _isLoopChangedSubscription: any;
    private _playlistChangedSubscription: any;

    public isPlaylistLoaded: boolean;
    public currentTime: number;
    public duration: number;

    public display: boolean;

    public playlists: Playlist[];
    public currentPlaylist: Playlist;
    public audio: Audiotrack;

    public isPlaying: boolean;

    public volume: number;

    public isLoop: boolean;

    public playListName: string;

    constructor(private _audioPlayerService: AudioPlayerService) {
        this.isPlaylistLoaded = false;
        this.display = false;

        this.isPlaying = this._audioPlayerService.isPlaying();

        this.volume = this._audioPlayerService.getVolume() * 100;
        //this.playlists = this._audioPlayerService.getPlaylists();
        this.currentPlaylist = this._audioPlayerService.getCurrentPlaylist();
        this.audio = this._audioPlayerService.getCurrentAudio();
        this.currentTime = this._audioPlayerService.getCurrentTime();
        this.duration = this._audioPlayerService.getDuration();
        this.isLoop = this._audioPlayerService.isLoop();

        if (this.playlists) {
            this.isPlaylistLoaded = true;            
        } else {
            this._playlistLoadedSubscription = this._audioPlayerService.getOnPlayerLoadedEmitter()
                .subscribe(playlists => {
                    this.playlists = playlists;
                    this.isPlaylistLoaded = true;
                });
        }

        this._audioChangedSubscription = this._audioPlayerService.getOnAudioChanged()
            .subscribe(audio => this.audio = audio);

        this._durationChangeSubscription = this._audioPlayerService.getOnDurationEmitter()
            .subscribe(duration => this.duration = duration);

        this._timeUpdateSubscription = this._audioPlayerService.getOnTimeUpdateEmitter()
            .subscribe(currentTime => this.currentTime = currentTime);

        this._actionChangedSubscription = this._audioPlayerService.getOnActionChanged()
            .subscribe(isPlaingAction => this.isPlaying = isPlaingAction);

        this._isLoopChangedSubscription = this._audioPlayerService.getOnIsLoopEmitter()
            .subscribe(isLoop => this.isLoop = isLoop);

        this._playlistChangedSubscription = this._audioPlayerService.getOnPlaylistChanged()
            .subscribe(currentPlaylist => this.currentPlaylist = currentPlaylist);
    }    

    showDialog() {
        this.display = true;
    }

    hideDialog() {
        this.display = false;
    }

    play() {
        this._audioPlayerService.play();
    }

    playById(id: string) {
        this._audioPlayerService.playById(id);
    }

    pause() {
        this._audioPlayerService.pause();
    }

    next() {
        this._audioPlayerService.next();
    }

    toggleLoop() {
        this._audioPlayerService.setIsLoop(!this.isLoop);
    }

    previous() {
        this._audioPlayerService.previous();
    }

    volumeChanged(value: number) {
        this.volume = value;
        this._audioPlayerService.setVolume(this.volume / 100);
    }

    currentTimeChanged(value: number) {
        this.currentTime = value;
        this._audioPlayerService.setCurrentTime(this.currentTime);
    }

    selectPlaylist(id: string) {
        this._audioPlayerService.selectPlaylist(id);
    }

    createPlaylist(playlist: Playlist) {
        this._audioPlayerService.createPlaylist(playlist.title);
    }

    updatePlaylist(playlist: Playlist) {
        this._audioPlayerService.updatePlaylist(playlist.id, playlist.title);
    }

    onNgDestroy() {
        this._playlistLoadedSubscription.unsubscribe();
        this._audioChangedSubscription.unsubscribe();
        this._durationChangeSubscription.unsubscribe();
        this._timeUpdateSubscription.unsubscribe();
        this._isLoopChangedSubscription.unsubscribe();
    }
}