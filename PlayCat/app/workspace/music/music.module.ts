import { NgModule } from '@angular/core';
import { AudiosComponent } from './audios/audios.component';
import { PlaylistComponent } from './playlist/playlist.component';
import { UploadComponent } from './upload/upload.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UploadService } from './upload/upload.service'
import { PlaylistService } from './playlist/playlist.service';
import { RouterModule } from '@angular/router';
import { MenuModule } from '../menu/menu.module';
import { ErrorModule } from '../../shared/components/error.module';
import { CommonModule } from '@angular/common';
import { AuthGuardService } from '../../shared/services/authGuard.service';

@NgModule({
    imports: [
        MenuModule,
        ErrorModule,
        CommonModule,
        ReactiveFormsModule,
        RouterModule.forChild([
            {
                path: 'audios',
                component: AudiosComponent,
                canActivate: [AuthGuardService],
            },
            {
                path: 'playlist',
                component: PlaylistComponent,
                canActivate: [AuthGuardService],
            },
            {
                path: 'upload',
                component: UploadComponent,
                canActivate: [AuthGuardService],
            }
        ]),
    ],
    declarations: [
        AudiosComponent,
        PlaylistComponent,
        UploadComponent,
    ],
    providers: [
        UploadService,
        PlaylistService,
    ],
})
export class MusicModule {
}