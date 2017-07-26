import { NgModule } from '@angular/core';
import { ProfileComponent } from './profile/profile.component';
import { UpdateProfileComponent } from './updateProfile/updateProfile.component';
import { MenuModule } from '../menu/menu.module';
import { RouterModule } from '@angular/router';

@NgModule({
    imports: [
        MenuModule,
        RouterModule.forChild([
            {
                path: 'profile',
                component: ProfileComponent,
            },
            {
                path: 'updateProfile',
                component: UpdateProfileComponent
            },
        ]),
    ],
    declarations: [
        ProfileComponent,
        UpdateProfileComponent,    
    ]
})
export class CabinetModule {

}