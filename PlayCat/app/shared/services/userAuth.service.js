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
var user_1 = require("../../data/user");
var authToken_1 = require("../../data/authToken");
var http_service_1 = require("./http.service");
var UserAuthService = (function () {
    function UserAuthService(_httpService) {
        this._httpService = _httpService;
        this._userLSKey = 'user';
        this._authTokenLSKey = 'authToken';
        this.setAuthToken(this.getAuthTokenLS());
        this.setUser(this.getUserLS());
    }
    UserAuthService.prototype.saveUserLS = function (user) {
        if (user === null) {
            localStorage.removeItem(this._userLSKey);
        }
        else {
            localStorage.setItem(this._userLSKey, JSON.stringify(user));
        }
    };
    UserAuthService.prototype.getUserLS = function () {
        var item = localStorage.getItem(this._userLSKey);
        if (item === null)
            return null;
        var jsonUserObject = JSON.parse(item);
        return Object.assign(new user_1.User(), jsonUserObject);
    };
    UserAuthService.prototype.saveAuthTokenLS = function (authToken) {
        if (authToken === null) {
            localStorage.removeItem(this._authTokenLSKey);
        }
        else {
            localStorage.setItem(this._authTokenLSKey, JSON.stringify(authToken));
        }
    };
    UserAuthService.prototype.getAuthTokenLS = function () {
        var item = localStorage.getItem(this._authTokenLSKey);
        if (item === null)
            return null;
        var jsonAuthTokenObject = JSON.parse(item);
        return Object.assign(new authToken_1.AuthToken(), jsonAuthTokenObject);
    };
    UserAuthService.prototype.setUser = function (user) {
        this._user = user;
        this.saveUserLS(user);
    };
    UserAuthService.prototype.getUser = function () {
        return this._user;
    };
    UserAuthService.prototype.setAuthToken = function (authToken) {
        if (authToken !== null) {
            this._httpService.updateAccessToken(authToken.id);
        }
        this._authToken = authToken;
        this.saveAuthTokenLS(authToken);
    };
    UserAuthService.prototype.getAuthToken = function () {
        return this._authToken;
    };
    UserAuthService.prototype.isValid = function () {
        return this._user !== null && this._authToken !== null;
    };
    return UserAuthService;
}());
UserAuthService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_service_1.HttpService])
], UserAuthService);
exports.UserAuthService = UserAuthService;
//# sourceMappingURL=userAuth.service.js.map