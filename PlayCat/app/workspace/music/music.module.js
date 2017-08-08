"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var audios_component_1 = require("./audios/audios.component");
var playlist_component_1 = require("./playlist/playlist.component");
var upload_component_1 = require("./upload/upload.component");
var forms_1 = require("@angular/forms");
var router_1 = require("@angular/router");
var menu_module_1 = require("../menu/menu.module");
var error_module_1 = require("../../shared/components/error.module");
var MusicModule = (function () {
    function MusicModule() {
    }
    return MusicModule;
}());
MusicModule = __decorate([
    core_1.NgModule({
        imports: [
            menu_module_1.MenuModule,
            error_module_1.ErrorModule,
            forms_1.ReactiveFormsModule,
            router_1.RouterModule.forChild([
                {
                    path: 'audios',
                    component: audios_component_1.AudiosComponent,
                },
                {
                    path: 'playlist',
                    component: playlist_component_1.PlaylistComponent
                },
                {
                    path: 'upload',
                    component: upload_component_1.UploadComponent,
                }
            ]),
        ],
        declarations: [
            audios_component_1.AudiosComponent,
            playlist_component_1.PlaylistComponent,
            upload_component_1.UploadComponent,
        ],
    })
], MusicModule);
exports.MusicModule = MusicModule;
//# sourceMappingURL=music.module.js.map