import { NgModule } from '@angular/core';
import { HttpModule, JsonpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { AuthService } from '../auth/auth.service';
import { HttpService } from '../shared/services/http.service';
import { FormService } from '../shared/services/form.service';
import { ValidationService } from '../shared/services/validation.service';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

import { SignInComponent } from './signIn/signIn.component';
import { SignUpComponent } from './signUp/signUp.component';

import { ErrorComponent } from '../shared/components/error.component';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        JsonpModule,
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
        ErrorComponent
    ],
    providers: [
        AuthService,
        HttpService,
        FormService,
        ValidationService
    ]
})
export class AuthModule {

}