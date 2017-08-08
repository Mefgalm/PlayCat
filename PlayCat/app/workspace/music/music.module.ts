import { NgModule } from '@angular/core';
import { AudiosComponent } from './audios/audios.component';
import { PlaylistComponent } from './playlist/playlist.component';
import { UploadComponent } from './upload/upload.component';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MenuModule } from '../menu/menu.module';
import { ErrorModule } from '../../shared/components/error.module';

@NgModule({
    imports: [
        MenuModule,
        ErrorModule,
        ReactiveFormsModule,
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