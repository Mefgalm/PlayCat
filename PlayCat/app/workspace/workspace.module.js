"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var cabinet_module_1 = require("./cabinet/cabinet.module");
var music_module_1 = require("./music/music.module");
var upload_service_1 = require("./music/upload/upload.service");
var WorkspaceModule = (function () {
    function WorkspaceModule() {
    }
    return WorkspaceModule;
}());
WorkspaceModule = __decorate([
    core_1.NgModule({
        imports: [
            cabinet_module_1.CabinetModule,
            music_module_1.MusicModule,
        ],
        providers: [
            upload_service_1.UploadService,
        ],
    })
], WorkspaceModule);
exports.WorkspaceModule = WorkspaceModule;
//# sourceMappingURL=workspace.module.js.map