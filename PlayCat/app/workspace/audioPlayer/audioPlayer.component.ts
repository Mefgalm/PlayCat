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

    public isPlaylistLoaded: boolean;
    public currentTime: number;
    public duration: number;

    public display: boolean;

    public playlist: Playlist;
    public audio: Audiotrack;

    public isPlaying: boolean;

    constructor(private _audioPlayerService: AudioPlayerService) {
        this.isPlaylistLoaded = false;
        this.display = false;

        this.isPlaying = this._audioPlayerService.isPlaying();

        this.playlist = this._audioPlayerService.getPlaylist();
        this.audio = this._audioPlayerService.getCurrentAudio();
        this.currentTime = this._audioPlayerService.getCurrentTime();
        this.duration = this._audioPlayerService.getDuration();

        if (this.playlist) {
            this.isPlaylistLoaded = true;            
        } else {
            this._playlistLoadedSubscription = this._audioPlayerService.getOnPlaylistLoadedEmitter()
                .subscribe(playlist => {
                    this.playlist = playlist;
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

    previous() {
        this._audioPlayerService.previous();
    }

    onNgDestroy() {
        this._playlistLoadedSubscription.unsubscribe();
        this._audioChangedSubscription.unsubscribe();
        this._durationChangeSubscription.unsubscribe();
        this._timeUpdateSubscription.unsubscribe();
    }
}