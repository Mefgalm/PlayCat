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
var forms_1 = require("@angular/forms");
var validation_service_1 = require("../../shared/services/validation.service");
var playlist_1 = require("../../data/playlist");
var form_service_1 = require("../../shared/services/form.service");
var PlaylistComponent = (function () {
    function PlaylistComponent(_fb, _validationService, _formService) {
        this._fb = _fb;
        this._validationService = _validationService;
        this._formService = _formService;
        this.createModelName = 'CreatePlaylistRequest';
        this.updateModelName = 'UpdatePlaylistRequest';
        this.onCreate = new core_1.EventEmitter();
        this.onEdit = new core_1.EventEmitter();
        this.onDelete = new core_1.EventEmitter();
        this.onSelect = new core_1.EventEmitter();
        this.createErrorsValidation = new Map();
        this.updateErrorsValidation = new Map();
        this.createPlaylistForm = this._fb.group({
            title: [null],
        });
        this.updatePlaylistForm = this._fb.group({
            playlistId: [null],
            title: [null],
        });
    }
    PlaylistComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._validationService
            .get(this.createModelName)
            .then(function (res) {
            _this.createErrorsValidation = res;
            _this.createPlaylistForm = _this._formService.buildFormGroup(res);
        });
        this._validationService
            .get(this.updateModelName)
            .then(function (res) {
            _this.updateErrorsValidation = res;
            _this.updatePlaylistForm = _this._formService.buildFormGroup(res);
        });
    };
    PlaylistComponent.prototype.ngOnChanges = function () {
        this.editPlaylists = new Array();
        for (var _i = 0, _a = this.playlists; _i < _a.length; _i++) {
            var pl = _a[_i];
            var playlistEdit = pl;
            playlistEdit.isEditing = false;
            this.editPlaylists.push(playlistEdit);
        }
    };
    PlaylistComponent.prototype.startEdit = function (playlistEdit) {
        this.editPlaylists.forEach(function (x) { return x.isEditing = false; });
        playlistEdit.isEditing = true;
        this.updatePlaylistForm.setValue({
            playlistId: playlistEdit.id,
            title: playlistEdit.title
        });
    };
    PlaylistComponent.prototype.delete = function (playlistEdit) {
        var playlist = new playlist_1.Playlist();
        playlist.id = playlistEdit.id;
        this.onDelete.emit(playlist);
    };
    PlaylistComponent.prototype.stopEdit = function (playlistEdit) {
        playlistEdit.isEditing = false;
    };
    PlaylistComponent.prototype.select = function (playlist) {
        console.log(playlist.id);
        this.onSelect.emit(playlist);
    };
    PlaylistComponent.prototype.create = function (_a) {
        var value = _a.value, valid = _a.valid;
        if (valid) {
            var playlist = new playlist_1.Playlist();
            playlist.title = value.title;
            this.onCreate.emit(playlist);
        }
        else {
            this._formService.markControlsAsDirty(this.createPlaylistForm);
        }
    };
    PlaylistComponent.prototype.update = function (_a) {
        var value = _a.value, valid = _a.valid;
        if (valid) {
            var playlist = new playlist_1.Playlist();
            playlist.title = value.title;
            playlist.id = value.playlistId;
            this.onEdit.emit(playlist);
        }
        else {
            this._formService.markControlsAsDirty(this.updatePlaylistForm);
        }
    };
    return PlaylistComponent;
}());
__decorate([
    core_1.Input(),
    __metadata("design:type", Array)
], PlaylistComponent.prototype, "playlists", void 0);
__decorate([
    core_1.Input(),
    __metadata("design:type", playlist_1.Playlist)
], PlaylistComponent.prototype, "currentPlaylist", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", core_1.EventEmitter)
], PlaylistComponent.prototype, "onCreate", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", core_1.EventEmitter)
], PlaylistComponent.prototype, "onEdit", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", core_1.EventEmitter)
], PlaylistComponent.prototype, "onDelete", void 0);
__decorate([
    core_1.Output(),
    __metadata("design:type", core_1.EventEmitter)
], PlaylistComponent.prototype, "onSelect", void 0);
PlaylistComponent = __decorate([
    core_1.Component({
        selector: 'playlist',
        templateUrl: './app/workspace/audioPlayer/playlist.component.html',
        styleUrls: ['./app/workspace/audioPlayer/playlist.component.css'],
    }),
    __metadata("design:paramtypes", [forms_1.FormBuilder,
        validation_service_1.ValidationService,
        form_service_1.FormService])
], PlaylistComponent);
exports.PlaylistComponent = PlaylistComponent;
//# sourceMappingURL=playlist.component.js.map