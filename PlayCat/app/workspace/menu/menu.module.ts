import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MenuComponent } from './menu.component';
import { RouterModule } from '@angular/router';
import { AudioPlayerComponent } from '../audioPlayer/audioPlayer.component';
import { AudioPlayerModule } from '../audioPlayer/audioPlayer.module';

@NgModule({
    imports: [
        RouterModule,
        CommonModule,
        AudioPlayerModule,
    ],
    declarations: [
        MenuComponent,
    ],
    exports: [
        MenuComponent,
    ],
})
export class MenuModule {

}