import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AudioPlayerComponent } from './audioPlayer.component';
import { AudioPlayerService } from './audioPlayer.service';
import { AudioService } from '../music/audios/audios.service';
import { ProgressBarModule } from '../../shared/components/progressBar.module';
import { DialogModule } from 'primeng/primeng';
import { SecondToTimePipe } from '../../shared/pipes/secondToTime.pipe';
import { PlaylistService } from './playlist.service';
import { ErrorModule } from '../../shared/components/error.module';
import { PlaylistComponent } from './playlist.component';
import { AudioComponent } from './audio.component';
import { InfiniteScrollModule } from 'angular2-infinite-scroll/angular2-infinite-scroll';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ErrorModule,
        ReactiveFormsModule,
        ProgressBarModule,
        DialogModule,
        InfiniteScrollModule
    ],
    declarations: [
        AudioPlayerComponent,
        SecondToTimePipe,
        PlaylistComponent,
        AudioComponent
    ],
    exports: [
        AudioPlayerComponent
    ],
    providers: [
        PlaylistService,
        AudioService
    ]
})
export class AudioPlayerModule {

}