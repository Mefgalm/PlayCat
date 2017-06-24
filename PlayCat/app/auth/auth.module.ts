import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { SignInComponent } from './signIn/signIn.component';
import { SignUpComponent } from './signUp/signUp.component';

@NgModule({
    declarations: [
        SignInComponent,
        SignUpComponent
    ],
    exports: [SignInComponent, SignUpComponent],
})
export class AuthModule {

}