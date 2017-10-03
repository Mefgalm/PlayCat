"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var AudioComponent = (function () {
    function AudioComponent() {
        this.onPlayStart = new core_1.EventEmitter();
        this.onSelectAudio = new core_1.EventEmitter();
        this.onRemoveFromPlaylist = new core_1.EventEmitter();
    }
    AudioComponent.prototype.play = function () {
        this.onPlayStart.emit();
    };
    AudioComponent.prototype.selectAudio = function () {
        this.onSelectAudio.emit(this.id);
    };
    AudioComponent.prototype.removeFromPlaylist = function () {
        this.onRemoveFromPlaylist.emit(this.id);
    };
    return AudioComponent;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", String)
], AudioComponent.prototype, "id", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", String)
], AudioComponent.prototype, "artist", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", String)
], AudioComponent.prototype, "song", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", Number)
], AudioComponent.prototype, "duration", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", Object)
], AudioComponent.prototype, "onPlayStart", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", Object)
], AudioComponent.prototype, "onSelectAudio", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", Object)
], AudioComponent.prototype, "onRemoveFromPlaylist", void 0);
AudioComponent = __decorate([
    core_1.Component({
        selector: 'audiotrack',
        templateUrl: './app/workspace/audioPlayer/audio.component.html',
        styleUrls: ['./app/workspace/audioPlayer/audio.component.css'],
    })
], AudioComponent);
exports.AudioComponent = AudioComponent;
//# sourceMappingURL=audio.component.js.map