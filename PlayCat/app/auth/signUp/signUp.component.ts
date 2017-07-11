import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';


import { SignUpRequest } from '../../data/request/signUpRequest';
import { SignUpInResult } from '../../data/response/signUpInResult';

@Component({
    selector: 'signUp',
    templateUrl: './app/auth/signUp/signUp.component.html',
    styleUrls: ['./app/auth/signUp/signUp.component.css'],
})

export class SignUpComponent {
    //public firstName: string;
    //public lastName: string;
    //public email: string;
    //public password: string;
    //public confirmPassword: string;
    //public verificationCode: string;

    public errors: Map<string, string[]>;
    public globalError: string;

    public signUpForm: FormGroup;

    constructor(private _fb: FormBuilder, private _authService: AuthService) {
        this.globalError = null;
        this.errors = new Map<string, string[]>();
    }

    ngOnInit() {
        this.signUpForm = this._fb.group({
            firstName: [null, Validators.required],
            lastName: [null, Validators.required],
            email: [null, Validators.required],
            password: [null, Validators.required],
            confirmPassword: [null, Validators.required],
            verificationCode: [null, Validators.required],
        });
    }

    //public OnSubmit() {
    //    var request = new SignUpRequest(
    //        this.firstName,
    //        this.lastName,
    //        this.email,
    //        this.password,
    //        this.confirmPassword,
    //        this.verificationCode);

    //    this._authService
    //        .signUp(request)
    //        .then(signUpInResult => {
    //            this.errors = signUpInResult.errors;
    //            if (!signUpInResult.ok) {
    //                this.globalError = signUpInResult.info;
    //            }
    //        });
    //}

    public save({ value, valid }: { value: any, valid: boolean }) {
        console.log(value);

        if (valid) {
          var request = new SignUpRequest(
              value.firstName,
              value.lastName,
              value.email,
              value.password,
              value.confirmPassword,
              value.verificationCode);

            this._authService
                .signUp(request)
                .then(signUpInResult => {
                    console.log(signUpInResult);
                    this.errors = signUpInResult.errors;
                    if (!signUpInResult.ok) {
                        this.globalError = signUpInResult.info;
                    }
                });
        }
    }
}