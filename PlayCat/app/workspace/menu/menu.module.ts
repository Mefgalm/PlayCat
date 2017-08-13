import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MenuComponent } from './menu.component';
import { RouterModule } from '@angular/router';
import { AudioPlayerComponent } from '../audioPlayer/audioPlayer.component';
import { SecondToTimePipe } from '../../shared/pipes/secondToTime.pipe';
import { DialogModule } from 'primeng/primeng';
import { ProgressBarComponent } from '../audioPlayer/progressBar.component';

@NgModule({
    imports: [
        RouterModule,
        CommonModule,
        DialogModule,
    ],
    declarations: [
        MenuComponent,
        AudioPlayerComponent,
        SecondToTimePipe,
        ProgressBarComponent,
    ],
    exports: [
        MenuComponent,
    ],
})
export class MenuModule {

}