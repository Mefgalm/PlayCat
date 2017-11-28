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
var search_component_1 = require("./search/search.component");
var upload_component_1 = require("./upload/upload.component");
var forms_1 = require("@angular/forms");
var upload_service_1 = require("./upload/upload.service");
var search_service_1 = require("./search/search.service");
var router_1 = require("@angular/router");
var menu_module_1 = require("../menu/menu.module");
var error_module_1 = require("../../shared/components/error.module");
var loader_module_1 = require("../../shared/components/loader.module");
var common_1 = require("@angular/common");
var authGuard_service_1 = require("../../shared/services/authGuard.service");
var forms_2 = require("@angular/forms");
var safe_pipe_1 = require("../../shared/pipes/safe.pipe");
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
            common_1.CommonModule,
            forms_2.FormsModule,
            loader_module_1.LoaderModule,
            forms_1.ReactiveFormsModule,
            router_1.RouterModule.forChild([
                {
                    path: 'audios',
                    component: audios_component_1.AudiosComponent,
                    canActivate: [authGuard_service_1.AuthGuardService],
                },
                {
                    path: 'search',
                    component: search_component_1.SearchComponent,
                    canActivate: [authGuard_service_1.AuthGuardService],
                },
                {
                    path: 'upload',
                    component: upload_component_1.UploadComponent,
                    canActivate: [authGuard_service_1.AuthGuardService],
                }
            ]),
        ],
        declarations: [
            audios_component_1.AudiosComponent,
            search_component_1.SearchComponent,
            upload_component_1.UploadComponent,
            safe_pipe_1.SafePipe,
        ],
        providers: [
            upload_service_1.UploadService,
            search_service_1.SearchService
        ],
    })
], MusicModule);
exports.MusicModule = MusicModule;
//# sourceMappingURL=music.module.js.map