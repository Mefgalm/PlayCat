import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { AppComponent } from './app.component';
import { SignInComponent } from './auth/signIn/signIn.component';
import { SignUpComponent } from './auth/signUp/signUp.component';

import { AuthModule } from './auth/auth.module';

@NgModule({
    imports: [
        BrowserModule,
        AuthModule,
        //FormsModule,
        RouterModule.forRoot([
            {
                path: '',
                redirectTo: 'signIn',
                pathMatch: 'full',
            },
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
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }