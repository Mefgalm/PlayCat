﻿import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { UserAuthService } from './shared/services/userAuth.service';
import { AuthModule } from './auth/auth.module';

@NgModule({
    imports: [
        BrowserModule,
        AuthModule,
        RouterModule.forRoot([
            {
                path: '',
                redirectTo: 'signIn',
                pathMatch: 'full',
            },
        ]),
    ],
    providers: [UserAuthService],
    declarations: [AppComponent],
    bootstrap: [AppComponent]
})
export class AppModule {
}