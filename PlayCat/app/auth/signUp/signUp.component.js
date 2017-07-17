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
var forms_1 = require("@angular/forms");
var form_service_1 = require("../../shared/services/form.service");
var validation_service_1 = require("../../shared/services/validation.service");
var signUpRequest_1 = require("../../data/request/signUpRequest");
var router_1 = require("@angular/router");
var SignUpComponent = (function () {
    function SignUpComponent(_fb, _router, _authService, _formService, _validationService) {
        this._fb = _fb;
        this._router = _router;
        this._authService = _authService;
        this._formService = _formService;
        this._validationService = _validationService;
        this.modelName = 'SignUpRequest';
        this.globalError = null;
        this.errors = new Map();
        this.signUpForm = this._fb.group({
            firstName: [null],
            lastName: [null],
            email: [null],
            password: [null],
            confirmPassword: [null],
            verificationCode: [null],
        });
    }
    SignUpComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._validationService
            .get(this.modelName)
            .then(function (res) {
            _this.errorsValidation = res;
            _this.signUpForm = _this._formService.buildFormGroup(res);
        });
    };
    SignUpComponent.prototype.save = function (_a) {
        var _this = this;
        var value = _a.value, valid = _a.valid;
        if (valid) {
            var request = new signUpRequest_1.SignUpRequest(value.firstName, value.lastName, value.email, value.password, value.confirmPassword, value.verificationCode);
            this._authService
                .signUp(request)
                .then(function (signUpInResult) {
                _this.errors = signUpInResult.errors;
                if (signUpInResult.ok) {
                    _this._router.navigate(['/signIn']);
                }
                else if (signUpInResult.showInfo) {
                    _this.globalError = signUpInResult.info;
                }
            });
        }
        else {
            this._formService.markControlsAsDirty(this.signUpForm);
        }
    };
    return SignUpComponent;
}());
SignUpComponent = __decorate([
    core_1.Component({
        selector: 'signUp',
        templateUrl: './app/auth/signUp/signUp.component.html',
        styleUrls: ['./app/auth/signUp/signUp.component.css'],
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder,
        router_1.Router,
        auth_service_1.AuthService,
        form_service_1.FormService,
        validation_service_1.ValidationService])
], SignUpComponent);
exports.SignUpComponent = SignUpComponent;
//# sourceMappingURL=signUp.component.js.map