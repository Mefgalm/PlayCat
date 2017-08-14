"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var common_1 = require("@angular/common");
var core_1 = require("@angular/core");
var forms_1 = require("@angular/forms");
var menu_component_1 = require("./menu.component");
var router_1 = require("@angular/router");
var audioPlayer_component_1 = require("../audioPlayer/audioPlayer.component");
var secondToTime_pipe_1 = require("../../shared/pipes/secondToTime.pipe");
var primeng_1 = require("primeng/primeng");
var progressBar_component_1 = require("../audioPlayer/progressBar.component");
var MenuModule = (function () {
    function MenuModule() {
    }
    return MenuModule;
}());
MenuModule = __decorate([
    core_1.NgModule({
        imports: [
            router_1.RouterModule,
            common_1.CommonModule,
            primeng_1.DialogModule,
            forms_1.FormsModule,
        ],
        declarations: [
            menu_component_1.MenuComponent,
            audioPlayer_component_1.AudioPlayerComponent,
            secondToTime_pipe_1.SecondToTimePipe,
            progressBar_component_1.ProgressBarComponent,
        ],
        exports: [
            menu_component_1.MenuComponent,
        ],
    })
], MenuModule);
exports.MenuModule = MenuModule;
//# sourceMappingURL=menu.module.js.map