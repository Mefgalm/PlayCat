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
var router_1 = require("@angular/router");
var workspace_component_1 = require("./workspace.component");
var audios_component_1 = require("./music/audios/audios.component");
var playlist_component_1 = require("./music/playlist/playlist.component");
var upload_component_1 = require("./music/upload/upload.component");
var profile_component_1 = require("./cabinet/profile/profile.component");
var updateProfile_component_1 = require("./cabinet/updateProfile/updateProfile.component");
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
            router_1.RouterModule.forChild([
                {
                    path: 'cat',
                    redirectTo: '/cat/audios',
                    pathMatch: 'full',
                },
                {
                    path: 'cat',
                    component: workspace_component_1.WorkspaceComponent,
                    children: [
                        {
                            path: 'audios',
                            component: audios_component_1.AudiosComponent
                        },
                        {
                            path: 'playlist',
                            component: playlist_component_1.PlaylistComponent
                        },
                        {
                            path: 'upload',
                            component: upload_component_1.UploadComponent
                        },
                        {
                            path: 'profile',
                            component: profile_component_1.ProfileComponent
                        },
                        {
                            path: 'updateProfile',
                            component: updateProfile_component_1.UpdateProfileComponent
                        }
                    ]
                }
            ])
        ],
        declarations: [
            workspace_component_1.WorkspaceComponent
        ]
    })
], WorkspaceModule);
exports.WorkspaceModule = WorkspaceModule;
//# sourceMappingURL=workspace.module.js.map