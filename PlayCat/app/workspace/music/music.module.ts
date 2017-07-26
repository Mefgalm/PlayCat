import { NgModule } from '@angular/core';
import { AudiosComponent } from './audios/audios.component';
import { PlaylistComponent } from './playlist/playlist.component';
import { UploadComponent } from './upload/upload.component';
import { RouterModule } from '@angular/router';
import { MenuModule } from '../menu/menu.module';

@NgModule({
    imports: [
        MenuModule,
        RouterModule.forChild([
            {
                path: 'audios',
                component: AudiosComponent,
            },
            {
                path: 'playlist',
                component: PlaylistComponent
            },
            {
                path: 'upload',
                component: UploadComponent,
            }
        ]),
    ],
    declarations: [
        AudiosComponent,
        PlaylistComponent,
        UploadComponent,
    ],
})
export class MusicModule {
}