import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { UserAuthService } from './shared/services/userAuth.service';
import { AuthModule } from './auth/auth.module';
import { WorkspaceModule } from './workspace/workspace.module';
//import { ErrorModule } from './shared/components/error.module';

@NgModule({
    imports: [
        BrowserModule,
        AuthModule,
        //ErrorModule,
        CommonModule,
        WorkspaceModule,
        RouterModule.forRoot([
            {
                path: '',
                redirectTo: 'signIn',
                pathMatch: 'full',
            },
        ]),
    ],
    providers: [UserAuthService],
    declarations: [
        AppComponent,
    ],
    bootstrap: [AppComponent],    
})
export class AppModule {
}