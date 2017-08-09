"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var platform_browser_1 = require("@angular/platform-browser");
var auth_service_1 = require("../auth/auth.service");
var form_service_1 = require("../shared/services/form.service");
var validation_service_1 = require("../shared/services/validation.service");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var userAuth_service_1 = require("../shared/services/userAuth.service");
var signIn_component_1 = require("./signIn/signIn.component");
var signUp_component_1 = require("./signUp/signUp.component");
var error_module_1 = require("../shared/components/error.module");
var AuthModule = (function () {
    function AuthModule() {
    }
    return AuthModule;
}());
AuthModule = __decorate([
    core_1.NgModule({
        imports: [
            platform_browser_1.BrowserModule,
            http_1.HttpModule,
            http_1.JsonpModule,
            error_module_1.ErrorModule,
            forms_1.ReactiveFormsModule,
            router_1.RouterModule.forChild([
                {
                    path: 'signIn',
                    component: signIn_component_1.SignInComponent,
                },
                {
                    path: 'signUp',
                    component: signUp_component_1.SignUpComponent,
                }
            ]),
        ],
        declarations: [
            signIn_component_1.SignInComponent,
            signUp_component_1.SignUpComponent,
        ],
        providers: [
            auth_service_1.AuthService,
            form_service_1.FormService,
            validation_service_1.ValidationService,
            userAuth_service_1.UserAuthService
        ]
    })
], AuthModule);
exports.AuthModule = AuthModule;
//# sourceMappingURL=auth.module.js.map