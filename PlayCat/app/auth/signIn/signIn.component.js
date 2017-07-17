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
var form_service_1 = require("../../shared/services/form.service");
var forms_1 = require("@angular/forms");
var validation_service_1 = require("../../shared/services/validation.service");
var signInRequest_1 = require("../../data/request/signInRequest");
var SignInComponent = (function () {
    function SignInComponent(_fb, _authService, _formService, _validationService) {
        this._fb = _fb;
        this._authService = _authService;
        this._formService = _formService;
        this._validationService = _validationService;
        this.modelName = 'signUpRequest';
        this.globalError = null;
        this.errors = new Map();
        this.errorsValidation = new Map();
        this.errorsValidation['email'] = {
            required: 'Field email is required',
            pattern: 'Pattern is invalid'
        };
        this.errorsValidation['password'] = {
            required: 'Field password is required',
        };
    }
    SignInComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._validationService
            .get(this.modelName)
            .then(function (res) { return _this.errorsValidation = res; });
        this.signInForm = this._fb.group({
            email: [null, forms_1.Validators.required],
            password: [null, forms_1.Validators.required]
        });
    };
    SignInComponent.prototype.save = function (_a) {
        var _this = this;
        var value = _a.value, valid = _a.valid;
        if (valid) {
            var request = new signInRequest_1.SignInRequest(value.email, value.password);
            this._authService
                .signIn(request)
                .then(function (signUpInResult) {
                console.log(signUpInResult);
                _this.errors = signUpInResult.errors;
                if (!signUpInResult.ok) {
                    _this.globalError = signUpInResult.info;
                }
            });
        }
        else {
            this._formService.markControlsAsDirty(this.signInForm);
        }
    };
    return SignInComponent;
}());
SignInComponent = __decorate([
    core_1.Component({
        selector: 'signIn',
        templateUrl: './app/auth/signIn/signIn.component.html',
        styleUrls: ['./app/auth/signIn/signIn.component.css'],
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder,
        auth_service_1.AuthService,
        form_service_1.FormService,
        validation_service_1.ValidationService])
], SignInComponent);
exports.SignInComponent = SignInComponent;
//# sourceMappingURL=signIn.component.js.map