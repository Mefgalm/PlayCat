import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MenuComponent } from './menu.component';
import { RouterModule } from '@angular/router';
import { AudioPlayerComponent } from '../audioPlayer/audioPlayer.component';
import { SecondToTimePipe } from '../../shared/pipes/secondToTime.pipe';

@NgModule({
    imports: [
        RouterModule,
        CommonModule
    ],
    declarations: [
        MenuComponent,
        AudioPlayerComponent,
        SecondToTimePipe,
    ],
    exports: [
        MenuComponent,
    ],
})
export class MenuModule {

}