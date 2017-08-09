import { NgModule } from '@angular/core';
import { CabinetModule } from './cabinet/cabinet.module';
import { MusicModule } from './music/music.module';
import { RouterModule } from '@angular/router';
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
})
export class WorkspaceModule {

}