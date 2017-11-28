import { NgModule } from '@angular/core';
import { AudiosComponent } from './audios/audios.component';
import { SearchComponent } from './search/search.component';
import { UploadComponent } from './upload/upload.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UploadService } from './upload/upload.service';
import { SearchService } from './search/search.service';
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
                path: 'search',
                component: SearchComponent,
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
        SearchComponent,
        UploadComponent,
        SafePipe,     
    ],
    providers: [
        UploadService,
        SearchService
    ],
})
export class MusicModule {
}