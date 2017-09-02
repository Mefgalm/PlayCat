"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var progressBar_component_1 = require("./progressBar.component");
var ProgressBarModule = (function () {
    function ProgressBarModule() {
    }
    return ProgressBarModule;
}());
ProgressBarModule = __decorate([
    core_1.NgModule({
        declarations: [
            progressBar_component_1.ProgressBarComponent,
        ],
        exports: [
            progressBar_component_1.ProgressBarComponent,
        ],
    })
], ProgressBarModule);
exports.ProgressBarModule = ProgressBarModule;
//# sourceMappingURL=progressBar.module.js.map