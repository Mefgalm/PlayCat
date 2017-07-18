import { Injectable } from '@angular/core';
import { User } from '../../data/user';
import { AuthToken } from '../../data/authToken';

@Injectable()
export class UserAuthService {
    private _user: User;
    private _authToken: AuthToken;

    constructor(user: User, authToken: AuthToken) {
        this._user = user;
        this._authToken = authToken;
    }

    public setUser(user: User) {
        this._user = user;
    }

    public getUser(): User {
        return this._user;
    }

    public setAuthToken(authToken: AuthToken) {
        this._authToken = authToken;
    }

    public getAuthToken(): AuthToken {
        return this._authToken;
    }
}