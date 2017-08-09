"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var userAuth_service_1 = require("./userAuth.service");
var AuthGuardService = (function () {
    function AuthGuardService(_userAuthService, _router) {
        this._userAuthService = _userAuthService;
        this._router = _router;
    }
    AuthGuardService.prototype.canActivate = function (route, state) {
        if (this._userAuthService.isValid()) {
            return true;
        }
        else {
            this._router.navigate(['/signIn']);
            return false;
        }
    };
    return AuthGuardService;
}());
AuthGuardService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [userAuth_service_1.UserAuthService, router_1.Router])
], AuthGuardService);
exports.AuthGuardService = AuthGuardService;
//# sourceMappingURL=authGuard.service.js.map