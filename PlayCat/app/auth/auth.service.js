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
var signUpInResult_1 = require("../data/response/signUpInResult");
var http_service_1 = require("../shared/services/http.service");
require("rxjs/add/operator/toPromise");
var AuthService = (function () {
    function AuthService(_httpService) {
        this._httpService = _httpService;
        this._signInUrl = 'api/auth/signIn';
        this._signUpUrl = 'api/auth/signUp';
    }
    AuthService.prototype.signUp = function (request) {
        return this._httpService.post(this._signUpUrl, JSON.stringify(request))
            .then(function (x) { return Object.assign(new signUpInResult_1.SignUpInResult(), x.json()); });
    };
    AuthService.prototype.signIn = function (request) {
        return this._httpService.post(this._signInUrl, JSON.stringify(request))
            .then(function (x) { return Object.assign(new signUpInResult_1.SignUpInResult(), x.json()); });
    };
    return AuthService;
}());
AuthService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_service_1.HttpService])
], AuthService);
exports.AuthService = AuthService;
//# sourceMappingURL=auth.service.js.map