import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
    selector: 'signUp',
    templateUrl: './app/auth/signUp/signUp.component.html',
    styleUrls: ['./app/auth/signUp/signUp.component.css'],
})

export class SignUpComponent {
    constructor(private _authService: AuthService) {
        this._authService.signUp().then(x => console.log(x));
    }

    public OnSubmit() {
        this._authService.signUp().then(x => console.log(x));
    }
}