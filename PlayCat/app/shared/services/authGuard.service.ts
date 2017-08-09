import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { UserAuthService } from './userAuth.service';

@Injectable()
export class AuthGuardService implements CanActivate {
    constructor(private _userAuthService: UserAuthService, private _router: Router) {
    }

    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        if (this._userAuthService.isValid()) {
            return true;
        } else {
            this._router.navigate(['/signIn']);
            return false;
        }
    }
}