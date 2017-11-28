import { NgModule } from '@angular/core';
import { CabinetModule } from './cabinet/cabinet.module';
import { MusicModule } from './music/music.module';
import { RouterModule } from '@angular/router';
import { AudiosComponent } from './music/audios/audios.component';
import { UploadComponent } from './music/upload/upload.component';
import { ProfileComponent } from './cabinet/profile/profile.component';
import { UpdateProfileComponent } from './cabinet/updateProfile/updateProfile.component';
import { AudioPlayerService } from './audioPlayer/audioPlayer.service';

@NgModule({
    imports: [
        CabinetModule,
        MusicModule,
    ],
    providers: [
        AudioPlayerService,
        //PlaylistService
    ]
})
export class WorkspaceModule {

}