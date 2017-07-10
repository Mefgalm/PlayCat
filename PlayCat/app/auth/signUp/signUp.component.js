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
var auth_service_1 = require("../auth.service");
var signUpRequest_1 = require("../../data/request/signUpRequest");
var SignUpComponent = (function () {
    function SignUpComponent(_authService) {
        this._authService = _authService;
        this.globalError = null;
        this.errors = new Map();
    }
    SignUpComponent.prototype.OnSubmit = function () {
        var _this = this;
        var request = new signUpRequest_1.SignUpRequest(this.firstName, this.lastName, this.email, this.password, this.confirmPassword, this.verificationCode);
        this._authService
            .signUp(request)
            .then(function (signUpInResult) {
            _this.errors = signUpInResult.errors;
            if (!signUpInResult.ok) {
                _this.globalError = signUpInResult.info;
            }
        });
    };
    return SignUpComponent;
}());
SignUpComponent = __decorate([
    core_1.Component({
        selector: 'signUp',
        templateUrl: './app/auth/signUp/signUp.component.html',
        styleUrls: ['./app/auth/signUp/signUp.component.css'],
    }),
    __metadata("design:paramtypes", [auth_service_1.AuthService])
], SignUpComponent);
exports.SignUpComponent = SignUpComponent;
//# sourceMappingURL=signUp.component.js.map