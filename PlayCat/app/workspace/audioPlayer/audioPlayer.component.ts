import { Component, ViewChild } from '@angular/core';
import { AudioPlayerService } from './audioPlayer.service';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
    selector: 'audioPlayer',
    templateUrl: './app/workspace/audioPlayer/audioPlayer.component.html',
    styleUrls: ['./app/workspace/audioPlayer/audioPlayer.component.css'],
})
export class AudioPlayerComponent {
    @ViewChild('progressBar') progressBar;

    private onPlaylistLoadedSubscription: any;

    public isLoaded: boolean;
    public isPlaying: boolean;

    public currentTime: number;
    public duration: number;

    private _audio: any;

    constructor(public audioPlayerService: AudioPlayerService, private _sanitizer: DomSanitizer) {
        this.isLoaded = false;
        this.isPlaying = false;
        this.currentTime = 0;

        this._audio = new Audio();

        this.onPlaylistLoadedSubscription = audioPlayerService.getOnLoadedEmitter()
            .subscribe(isLoaded => {
                this.isLoaded = isLoaded;
                this.selectSource();

                this.registerEvents();
            });
    }

    private registerEvents() {
        this._audio.onended = () => this.next();
        this._audio.ondurationchange = () => this.duration = this._audio.duration;
        this._audio.ontimeupdate = () => this.currentTime = this._audio.currentTime;
    }

    ngOnDestroy() {
        this.onPlaylistLoadedSubscription.unsubscribe();
    }

    private selectSource() {
        this._audio.src = this.audioPlayerService.getCurrentAudio().accessUrl;
    }

    next() {
        this.audioPlayerService.playNext();

        this.selectSource();
        if (this.isPlaying) {
            this.play();
        }
    }

    prev() {
        this.audioPlayerService.playPrev();

        this.selectSource();
        if (this.isPlaying) {
            this.play();
        }
    }

    pause() {
        this._audio.pause();

        this.isPlaying = false;
    }

    play() {
        this._audio.play();

        this.isPlaying = true;
    }

    setCurrentTime(value: number) {
        this.pause();
        this._audio.currentTime = 1 * value;
        this.play();
    }

    setVolume(value: number) {
        this._audio.volume = value / 100;
    }
}