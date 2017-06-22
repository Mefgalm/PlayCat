import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { SignInComponent } from './signIn/signIn.component';
import { SpaceComponent } from './space/space.component';

@NgModule({
    imports: [
        BrowserModule,
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
                path: 'space',
                component: SpaceComponent,
            }
        ]),
    ],
    declarations: [AppComponent, SignInComponent, SpaceComponent],
    bootstrap: [AppComponent]
})
export class AppModule { }