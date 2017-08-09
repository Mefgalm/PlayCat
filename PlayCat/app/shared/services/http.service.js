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
require("rxjs/add/operator/toPromise");
var HttpService = (function () {
    function HttpService(_http) {
        this._http = _http;
        this.headers = new http_1.Headers();
        this.headers.set('Content-Type', 'application/json');
    }
    HttpService.prototype.updateAccessToken = function (accessToken) {
        this.headers.set('AccessToken', accessToken);
    };
    HttpService.prototype.post = function (url, jsonBody) {
        return this._http.post(url, jsonBody, { headers: this.headers }).toPromise();
    };
    HttpService.prototype.get = function (url) {
        return this._http.get(url, { headers: this.headers }).toPromise();
    };
    HttpService.prototype.delete = function (url) {
        return this._http.delete(url, { headers: this.headers }).toPromise();
    };
    HttpService.prototype.put = function (url, jsonBody) {
        return this._http.put(url, jsonBody, { headers: this.headers }).toPromise();
    };
    HttpService.prototype.buildParametersUrl = function () {
        var args = [];
        for (var _i = 0; _i < arguments.length; _i++) {
            args[_i] = arguments[_i];
        }
        if (args === null || args.length === 0)
            return '';
        return '?' +
            args.filter(function (x) { return x.value !== null; })
                .map(function (x) { return x.key + '=' + x.value; })
                .join('&');
    };
    return HttpService;
}());
HttpService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], HttpService);
exports.HttpService = HttpService;
//# sourceMappingURL=http.service.js.map