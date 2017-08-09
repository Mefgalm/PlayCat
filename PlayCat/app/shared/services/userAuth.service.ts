import { Injectable } from '@angular/core';
import { User } from '../../data/user';
import { AuthToken } from '../../data/authToken';
import { HttpService } from './http.service';

@Injectable()
export class UserAuthService {
    private _user: User;
    private _authToken: AuthToken;

    private _userLSKey = 'user';
    private _authTokenLSKey = 'authToken';

    constructor(private _httpService: HttpService) {
        this.setAuthToken(this.getAuthTokenLS());
        this.setUser(this.getUserLS());
    }

    private saveUserLS(user: User) {
        if (user === null) {
            localStorage.removeItem(this._userLSKey);
        } else {
            localStorage.setItem(this._userLSKey, JSON.stringify(user));
        }
    }

    private getUserLS(): User {
        let item = localStorage.getItem(this._userLSKey);

        if (item === null)
            return null;

        let jsonUserObject = JSON.parse(item);
        return Object.assign(new User(), jsonUserObject);
    }

    private saveAuthTokenLS(authToken: AuthToken) {
        if (authToken === null) {
            localStorage.removeItem(this._authTokenLSKey);
        } else {
            localStorage.setItem(this._authTokenLSKey, JSON.stringify(authToken));
        }
    }

    private getAuthTokenLS(): AuthToken {
        let item = localStorage.getItem(this._authTokenLSKey);

        if (item === null)
            return null;

        let jsonAuthTokenObject = JSON.parse(item);
        return Object.assign(new AuthToken(), jsonAuthTokenObject);
    }


    public setUser(user: User) {
        this._user = user;

        this.saveUserLS(user);
    }

    public getUser(): User {
        return this._user;
    }

    public setAuthToken(authToken: AuthToken) {
        if (authToken !== null) {
            this._httpService.updateAccessToken(authToken.id);
        }

        this._authToken = authToken;
        this.saveAuthTokenLS(authToken);
    }

    public getAuthToken(): AuthToken {
        return this._authToken;
    }

    public isValid(): boolean {
        return this._user !== null && this._authToken !== null;
    }
}