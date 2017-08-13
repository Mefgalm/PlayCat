import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { UserAuthService } from './shared/services/userAuth.service';
import { AuthModule } from './auth/auth.module';
import { WorkspaceModule } from './workspace/workspace.module';
import { HttpService } from './shared/services/http.service';
import { AuthGuardService } from './shared/services/authGuard.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AuthModule,
        WorkspaceModule,
        RouterModule.forRoot([
            {
                path: '',
                redirectTo: 'signIn',
                pathMatch: 'full',
            },
        ]),
    ],
    providers: [
        UserAuthService,
        HttpService,
        AuthGuardService
    ],
    declarations: [
        AppComponent,
    ],
    bootstrap: [AppComponent],    
})
export class AppModule {
} 