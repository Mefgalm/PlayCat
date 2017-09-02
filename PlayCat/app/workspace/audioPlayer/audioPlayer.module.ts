import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AudioPlayerComponent } from './audioPlayer.component';
import { AudioPlayerService } from './audioPlayer.service';
import { ProgressBarModule } from '../../shared/components/progressBar.module';
import { DialogModule } from 'primeng/primeng';
import { SecondToTimePipe } from '../../shared/pipes/secondToTime.pipe';
import { PlaylistService } from './playlist.service';
import { ErrorModule } from '../../shared/components/error.module';
import { PlaylistComponent } from './playlist.component';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ErrorModule,
        ReactiveFormsModule,
        ProgressBarModule,
        DialogModule
    ],
    declarations: [
        AudioPlayerComponent,
        SecondToTimePipe,
        PlaylistComponent
    ],
    exports: [
        AudioPlayerComponent
    ],
    providers: [
        PlaylistService
    ]
})
export class AudioPlayerModule {

}