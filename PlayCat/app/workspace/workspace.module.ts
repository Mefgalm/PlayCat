import { NgModule } from '@angular/core';
import { CabinetModule } from './cabinet/cabinet.module';
import { MusicModule } from './music/music.module';
import { RouterModule } from '@angular/router';
import { WorkspaceComponent } from './workspace.component';
import { AudiosComponent } from './music/audios/audios.component';
import { PlaylistComponent } from './music/playlist/playlist.component';
import { UploadComponent } from './music/upload/upload.component';
import { ProfileComponent } from './cabinet/profile/profile.component';
import { UpdateProfileComponent } from './cabinet/updateProfile/updateProfile.component';

@NgModule({
    imports: [
        CabinetModule,
        MusicModule,
        RouterModule.forChild([
            {
                path: 'cat',
                redirectTo: '/cat/audios',
                pathMatch: 'full',
            },
            {
                path: 'cat',
                component: WorkspaceComponent,
                children: [
                    {
                        path: 'audios',
                        component: AudiosComponent
                    },
                    {
                        path: 'playlist',
                        component: PlaylistComponent
                    },
                    {
                        path: 'upload',
                        component: UploadComponent
                    },
                    {
                        path: 'profile',
                        component: ProfileComponent
                    },
                    {
                        path: 'updateProfile',
                        component: UpdateProfileComponent
                    }
                ]
            }
        ])
    ],
    declarations: [
        WorkspaceComponent
    ]
})
export class WorkspaceModule {

}