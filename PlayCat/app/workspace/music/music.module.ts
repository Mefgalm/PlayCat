import { NgModule } from '@angular/core';
import { AudiosComponent } from './audios/audios.component';
import { PlaylistComponent } from './playlist/playlist.component';
import { UploadComponent } from './upload/upload.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UploadService } from './upload/upload.service'
import { RouterModule } from '@angular/router';
import { MenuModule } from '../menu/menu.module';
import { ErrorModule } from '../../shared/components/error.module';
import { LoaderModule } from '../../shared/components/loader.module';
import { CommonModule } from '@angular/common';
import { AuthGuardService } from '../../shared/services/authGuard.service';
import { FormsModule } from '@angular/forms';
import { SafePipe } from '../../shared/pipes/safe.pipe';

@NgModule({
    imports: [
        MenuModule,
        ErrorModule,
        CommonModule,
        FormsModule,
        LoaderModule,
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
        SafePipe,     
    ],
    providers: [
        UploadService,
    ],
})
export class MusicModule {
}