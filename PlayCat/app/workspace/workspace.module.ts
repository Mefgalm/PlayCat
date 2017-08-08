import { NgModule } from '@angular/core';
import { CabinetModule } from './cabinet/cabinet.module';
import { MusicModule } from './music/music.module';
import { RouterModule } from '@angular/router';
import { UploadService } from './music/upload/upload.service'
import { AudiosComponent } from './music/audios/audios.component';
import { PlaylistComponent } from './music/playlist/playlist.component';
import { UploadComponent } from './music/upload/upload.component';
import { ProfileComponent } from './cabinet/profile/profile.component';
import { UpdateProfileComponent } from './cabinet/updateProfile/updateProfile.component';

@NgModule({
    imports: [
        CabinetModule,
        MusicModule,
    ],
    providers: [
        UploadService,
    ],
})
export class WorkspaceModule {

}