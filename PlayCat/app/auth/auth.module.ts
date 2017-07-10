import { NgModule } from '@angular/core';
import { HttpModule, JsonpModule } from '@angular/http';
import { BrowserModule } from '@angular/platform-browser';
import { AuthService } from '../auth/auth.service';
import { FormsModule } from '@angular/forms';

import { SignInComponent } from './signIn/signIn.component';
import { SignUpComponent } from './signUp/signUp.component';

@NgModule({
    imports: [
        BrowserModule,
        HttpModule,
        JsonpModule,
        FormsModule
    ],
    declarations: [
        SignInComponent,
        SignUpComponent
    ],
    exports: [SignInComponent, SignUpComponent],
    providers: [AuthService],
})
export class AuthModule {

}