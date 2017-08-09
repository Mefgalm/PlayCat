import { NgModule } from '@angular/core';
import { HttpModule, JsonpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { AuthService } from '../auth/auth.service';
import { FormService } from '../shared/services/form.service';
import { ValidationService } from '../shared/services/validation.service';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { UserAuthService } from '../shared/services/userAuth.service';

import { SignInComponent } from './signIn/signIn.component';
import { SignUpComponent } from './signUp/signUp.component';
import { ErrorModule } from '../shared/components/error.module';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        JsonpModule,
        ErrorModule,
        ReactiveFormsModule,
        RouterModule.forChild([
            {
                path: 'signIn',
                component: SignInComponent,
            },
            {
                path: 'signUp',
                component: SignUpComponent,
            }
        ]),
    ],
    declarations: [
        SignInComponent,
        SignUpComponent,
    ],
    providers: [
        AuthService,
        FormService,
        ValidationService,
        UserAuthService
    ]
})
export class AuthModule {

}