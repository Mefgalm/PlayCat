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
var http_1 = require("@angular/http");
var signUpInResult_1 = require("../data/response/signUpInResult");
require("rxjs/add/operator/toPromise");
var AuthService = (function () {
    function AuthService(_http) {
        this._http = _http;
        this._signUpUrl = 'api/auth/signUp';
        this.headers = new http_1.Headers();
        this.headers.append('Content-Type', 'application/json');
    }
    AuthService.prototype.signUp = function (request) {
        var object = JSON.stringify(request);
        return this._http.post(this._signUpUrl, object, { headers: this.headers })
            .toPromise()
            .then(function (x) { return Object.assign(new signUpInResult_1.SignUpInResult(), x.json()); });
    };
    return AuthService;
}());
AuthService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], AuthService);
exports.AuthService = AuthService;
//# sourceMappingURL=auth.service.js.map