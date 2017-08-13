"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var platform_browser_1 = require("@angular/platform-browser");
var router_1 = require("@angular/router");
var app_component_1 = require("./app.component");
var userAuth_service_1 = require("./shared/services/userAuth.service");
var auth_module_1 = require("./auth/auth.module");
var workspace_module_1 = require("./workspace/workspace.module");
var http_service_1 = require("./shared/services/http.service");
var authGuard_service_1 = require("./shared/services/authGuard.service");
var animations_1 = require("@angular/platform-browser/animations");
var AppModule = (function () {
    function AppModule() {
    }
    return AppModule;
}());
AppModule = __decorate([
    core_1.NgModule({
        imports: [
            platform_browser_1.BrowserModule,
            animations_1.BrowserAnimationsModule,
            auth_module_1.AuthModule,
            workspace_module_1.WorkspaceModule,
            router_1.RouterModule.forRoot([
                {
                    path: '',
                    redirectTo: 'signIn',
                    pathMatch: 'full',
                },
            ]),
        ],
        providers: [
            userAuth_service_1.UserAuthService,
            http_service_1.HttpService,
            authGuard_service_1.AuthGuardService
        ],
        declarations: [
            app_component_1.AppComponent,
        ],
        bootstrap: [app_component_1.AppComponent],
    })
], AppModule);
exports.AppModule = AppModule;
//# sourceMappingURL=app.module.js.map