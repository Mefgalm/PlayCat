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
var audioPlayer_component_1 = require("./audioPlayer.component");
var audios_service_1 = require("../music/audios/audios.service");
var progressBar_module_1 = require("../../shared/components/progressBar.module");
var primeng_1 = require("primeng/primeng");
var secondToTime_pipe_1 = require("../../shared/pipes/secondToTime.pipe");
var playlist_service_1 = require("./playlist.service");
var error_module_1 = require("../../shared/components/error.module");
var playlist_component_1 = require("./playlist.component");
var audio_component_1 = require("./audio.component");
var angular2_infinite_scroll_1 = require("angular2-infinite-scroll/angular2-infinite-scroll");
var AudioPlayerModule = (function () {
    function AudioPlayerModule() {
    }
    return AudioPlayerModule;
}());
AudioPlayerModule = __decorate([
    core_1.NgModule({
        imports: [
            common_1.CommonModule,
            forms_1.FormsModule,
            error_module_1.ErrorModule,
            forms_1.ReactiveFormsModule,
            progressBar_module_1.ProgressBarModule,
            primeng_1.DialogModule,
            angular2_infinite_scroll_1.InfiniteScrollModule
        ],
        declarations: [
            audioPlayer_component_1.AudioPlayerComponent,
            secondToTime_pipe_1.SecondToTimePipe,
            playlist_component_1.PlaylistComponent,
            audio_component_1.AudioComponent
        ],
        exports: [
            audioPlayer_component_1.AudioPlayerComponent
        ],
        providers: [
            playlist_service_1.PlaylistService,
            audios_service_1.AudioService
        ]
    })
], AudioPlayerModule);
exports.AudioPlayerModule = AudioPlayerModule;
//# sourceMappingURL=audioPlayer.module.js.map