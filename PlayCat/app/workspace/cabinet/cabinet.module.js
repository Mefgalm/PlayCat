"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var profile_component_1 = require("./profile/profile.component");
var updateProfile_component_1 = require("./updateProfile/updateProfile.component");
var menu_module_1 = require("../menu/menu.module");
var router_1 = require("@angular/router");
var CabinetModule = (function () {
    function CabinetModule() {
    }
    return CabinetModule;
}());
CabinetModule = __decorate([
    core_1.NgModule({
        imports: [
            menu_module_1.MenuModule,
            router_1.RouterModule.forChild([
                {
                    path: 'profile',
                    component: profile_component_1.ProfileComponent,
                },
                {
                    path: 'updateProfile',
                    component: updateProfile_component_1.UpdateProfileComponent
                },
            ]),
        ],
        declarations: [
            profile_component_1.ProfileComponent,
            updateProfile_component_1.UpdateProfileComponent,
        ]
    })
], CabinetModule);
exports.CabinetModule = CabinetModule;
//# sourceMappingURL=cabinet.module.js.map