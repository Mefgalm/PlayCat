import { NgModule } from '@angular/core';
import { AudiosComponent } from './audios/audios.component';
import { PlaylistComponent } from './playlist/playlist.component';
import { UploadComponent } from './upload/upload.component';

@NgModule({
    declarations: [AudiosComponent, PlaylistComponent, UploadComponent],
})
export class MusicModule {
}